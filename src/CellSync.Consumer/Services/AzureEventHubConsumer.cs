using System.Collections.Concurrent;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using CellSync.Consumer.EventProcessors;
using CellSync.Consumer.Models;
using CellSync.Consumer.Settings;

namespace CellSync.Consumer.Services;

public class AzureEventHubConsumer
{
    private readonly EventProcessorClient _processor;
    private CancellationTokenSource? _cancellationToken;
    private readonly ConcurrentDictionary<string, List<object>> _eventProcessors = new();

    public AzureEventHubConsumer(IEventHubSettings eventHubSettings)
    {
        var storageClient = new BlobContainerClient(
            connectionString: eventHubSettings.StorageConnectionString,
            blobContainerName: eventHubSettings.StorageContainerName
        );

        _processor = new EventProcessorClient(
            checkpointStore: storageClient,
            consumerGroup: EventHubConsumerClient.DefaultConsumerGroupName,
            connectionString: eventHubSettings.EventHubConnectionString,
            eventHubName: eventHubSettings.EventHubName
        );

        _processor.ProcessEventAsync += ProcessEventHandler;
        _processor.ProcessErrorAsync += ProcessErrorHandler;
    }

    private Task ProcessEventHandler(ProcessEventArgs args)
    {
        if (args.CancellationToken.IsCancellationRequested) return Task.CompletedTask;

        var eventBody = Encoding.UTF8.GetString(args.Data.Body.ToArray());
        var eventObject = JsonSerializer.Deserialize<AzureEvent<object>>(eventBody);
        var eventName = eventObject?.EventName;

        if (string.IsNullOrWhiteSpace(eventName) || eventObject?.EventData is null) return Task.CompletedTask;

        _eventProcessors.TryGetValue(eventName, out var processors);

        if (processors is null) return Task.CompletedTask;

        //TODO: VERIFICAR QUAIS OS POSSÍVEL PROBLEMAS DESSE CÓDIGO
        foreach (var processor in processors)
        {
            // REFLECTION https://learn.microsoft.com/pt-br/dotnet/fundamentals/reflection/reflection
            var processorType = processor.GetType();
            var eventDataType = processorType.GetInterfaces()[0].GetGenericArguments()[0];

            var eventData = JsonSerializer.Deserialize(eventObject.EventData.ToString()!, eventDataType);
            var method = processorType.GetMethod("OnReceiveEventAsync");

            if (method is null) continue;

            Task.Run(() => method.Invoke(processor, [eventData]));
        }

        return Task.CompletedTask;
    }

    private Task ProcessErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine("Error in EventProcessorClient");
        Console.WriteLine(args.Exception.ToString());

        return Task.CompletedTask;
    }

    public void Subscribe<TEventData>(string eventName, IEventProcessor<TEventData> processor)
    {
        _eventProcessors.AddOrUpdate(eventName, [processor], (_, existingList) =>
        {
            existingList.Add(processor);
            return existingList;
        });
    }

    public async Task StartAsync()
    {
        try
        {
            Console.WriteLine("Starting Azure Event Hub Consumer");
            _cancellationToken = new CancellationTokenSource();
            await _processor.StartProcessingAsync(_cancellationToken.Token);
            // TODO: VERIFICAR SE DAR PARA UTILIZAR COMO IHOSTED SERVICES (https://www.site24x7.com/learn/ihostedservice-and-backgroundservice.html)
            await Task.Delay(Timeout.Infinite, _cancellationToken.Token);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while starting EventHub Consumer");
            Console.WriteLine(e);
        }
        finally
        {
            Console.WriteLine("Stopping EventHub Consumer");
            await StopAsync();
        }
    }

    private async Task StopAsync()
    {
        if (_cancellationToken is not null)
            await _cancellationToken?.CancelAsync()!;

        await _processor.StopProcessingAsync();

        _processor.ProcessErrorAsync -= ProcessErrorHandler;
        _processor.ProcessEventAsync -= ProcessEventHandler;
    }
}