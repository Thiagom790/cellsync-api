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

    public async Task PublishAsync<TMessage>(TMessage message) where TMessage : IEventMessage
    {
        using var eventBatch = await _producerClient.CreateBatchAsync();
        var eventBody = JsonSerializer.Serialize(new { EventType = message.MessageType, EventData = message });
        eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(eventBody)));

        await _producerClient.SendAsync(eventBatch);
    }

    public async ValueTask DisposeAsync()
    {
        await _producerClient.DisposeAsync();
    }
}