using System.Text;
using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;

namespace CellSync.Infrastructure.Events;

public class EventHubPublisher(IEventHubSettings settings) : IEventPublisher, IAsyncDisposable
{
    private readonly EventHubProducerClient _producerClient = new(
        connectionString: settings.EventHubConnectionString,
        eventHubName: settings.EventHubName
    );

    public async Task PublishAsync<TEventMessage>(string eventType, TEventMessage message)
        where TEventMessage : IEventMessage
    {
        try
        {
            using var eventBatch = await _producerClient.CreateBatchAsync();
            var eventBody = JsonSerializer.Serialize(new { EventType = eventType, EventData = message });
            eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(eventBody)));

            await _producerClient.SendAsync(eventBatch);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _producerClient.DisposeAsync();
    }
}