using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using CellSync.Domain.Events;
using Microsoft.Extensions.Hosting;

namespace CellSync.Infrastructure.Events;

public class EventHubConsumer : BackgroundService
{
    private readonly EventProcessorClient _processor;
    private readonly ConcurrentDictionary<string, List<object>> _eventProcessors = new();

    public EventHubConsumer(IEventHubSettings settings)
    {
        var storageClient = new BlobContainerClient(
            connectionString: settings.StorageConnectionString,
            blobContainerName: settings.StorageContainerName
        );

        _processor = new EventProcessorClient(
            checkpointStore: storageClient,
            consumerGroup: EventHubConsumerClient.DefaultConsumerGroupName,
            connectionString: settings.EventHubConnectionString,
            eventHubName: settings.EventHubName
        );

        _processor.ProcessEventAsync += ProcessEventHandler;
        _processor.ProcessErrorAsync += ProcessErrorHandler;
    }

    private Task ProcessEventHandler(ProcessEventArgs args)
    {
        if (args.CancellationToken.IsCancellationRequested) return Task.CompletedTask;

        var eventBody = Encoding.UTF8.GetString(args.Data.Body.ToArray());
        
        Console.WriteLine("Backaground");
        Console.WriteLine(eventBody);
        
        //FIX: NÃO FUNCIONA
        // var eventObject = JsonSerializer.Deserialize<AzureEvent<object>>(eventBody);
        // var eventName = eventObject?.EventName;
        //
        // if (string.IsNullOrWhiteSpace(eventName) || eventObject?.EventData is null) return Task.CompletedTask;
        //
        // _eventProcessors.TryGetValue(eventName, out var processors);
        //
        // if (processors is null) return Task.CompletedTask;
        //
        // //TODO: VERIFICAR QUAIS OS POSSÍVEL PROBLEMAS DESSE CÓDIGO
        // foreach (var processor in processors)
        // {
        //     // REFLECTION https://learn.microsoft.com/pt-br/dotnet/fundamentals/reflection/reflection
        //     var processorType = processor.GetType();
        //     var eventDataType = processorType.GetInterfaces()[0].GetGenericArguments()[0];
        //
        //     var eventData = JsonSerializer.Deserialize(eventObject.EventData.ToString()!, eventDataType);
        //     var method = processorType.GetMethod("OnReceiveEventAsync");
        //
        //     if (method is null) continue;
        //
        //     Task.Run(() => method.Invoke(processor, [eventData]));
        // }

        return Task.CompletedTask;
    }

    private static Task ProcessErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine("Error in EventProcessorClient");
        Console.WriteLine(args.Exception.ToString());

        return Task.CompletedTask;
    }

    //FIX: NÃO FUNCIONA 
    public void Subscribe<TEventData>(string eventName, IEventProcessor<TEventData> processor)
    {
        _eventProcessors.AddOrUpdate(eventName, [processor], (_, existingList) =>
        {
            existingList.Add(processor);
            return existingList;
        });
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine("Starting Azure Event Hub Consumer");
            await _processor.StartProcessingAsync(cancellationToken);

            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while starting EventHub Consumer");
            Console.WriteLine(e);
        }
        finally
        {
            Console.WriteLine("Stopping Azure Event Hub Consumer");
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _processor.StopProcessingAsync(cancellationToken);

        _processor.ProcessErrorAsync -= ProcessErrorHandler;
        _processor.ProcessEventAsync -= ProcessEventHandler;

        await base.StopAsync(cancellationToken);
    }
}