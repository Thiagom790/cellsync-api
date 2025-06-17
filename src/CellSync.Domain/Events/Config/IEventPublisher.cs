using CellSync.Domain.Events.Messages;

namespace CellSync.Domain.Events.Config;

public interface IEventPublisher
{
    Task PublishAsync<TEventMessage>(string eventType, TEventMessage message) where TEventMessage : IEventMessage;
}