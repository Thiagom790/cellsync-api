using System.Text;
using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using CellSync.Domain.Events;

namespace CellSync.Infrastructure.Events;

public class EventHubPublisher(IEventHubSettings settings) : IEventPublisher, IAsyncDisposable
{
    private readonly EventHubProducerClient _producerClient = new(
        connectionString: settings.EventHubConnectionString,
        eventHubName: settings.EventHubName
    );

    public async Task PublishAsync(string eventName, object eventData)
    {
        using var eventBatch = await _producerClient.CreateBatchAsync();
        var eventBody = JsonSerializer.Serialize(new { EventName = eventName, EventData = eventData });
        eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(eventBody)));

        await _producerClient.SendAsync(eventBatch);
    }

    public async ValueTask DisposeAsync()
    {
        await _producerClient.DisposeAsync();
    }
}