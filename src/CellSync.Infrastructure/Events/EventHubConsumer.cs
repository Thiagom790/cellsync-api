using System.Text;
using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;
using Microsoft.Extensions.Hosting;

namespace CellSync.Infrastructure.Events;

public class EventHubConsumer : BackgroundService
{
    private readonly EventProcessorClient _processor;
    private readonly IEventDispatcher _dispatcher;

    public EventHubConsumer(IEventHubSettings settings, IEventDispatcher dispatcher)
    {
        _dispatcher = dispatcher;

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

    private async Task ProcessEventHandler(ProcessEventArgs args)
    {
        if (args.CancellationToken.IsCancellationRequested) return;

        using var json = JsonDocument.Parse(Encoding.UTF8.GetString(args.Data.Body.ToArray()));
        var eventName = json.RootElement.GetProperty("EventType").GetString();
        var rawEventData = json.RootElement.GetProperty("EventData");
        var eventType = _dispatcher.GetMessageType(eventName);

        if (eventType is null || eventName is null) return;

        var message = (IEventMessage)rawEventData.Deserialize(eventType)!;

        await _dispatcher.DispatchAsync(eventName, message);
    }

    private static Task ProcessErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine("Error in EventProcessorClient");
        Console.WriteLine(args.Exception.ToString());

        return Task.CompletedTask;
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