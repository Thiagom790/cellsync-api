using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;

namespace CellSync.Infrastructure.Events;

public class EventPublisher(InMemoryMessageQueue queue) : IEventPublisher
{
    public async Task PublishAsync<TEventMessage>(TEventMessage message) where TEventMessage : IEventMessage
    {
        await queue.EnqueueAsync(message);
    }
}