using CellSync.Domain.Events.Messages;

namespace CellSync.Domain.Events.Config;

public interface IEventPublisher
{
    Task PublishAsync<TMessage>(TMessage message) where TMessage : IEventMessage;
}