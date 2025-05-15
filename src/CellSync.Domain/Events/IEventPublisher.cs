namespace CellSync.Domain.Events;

public interface IEventPublisher
{
    Task PublishAsync(string eventName, object eventData);
}