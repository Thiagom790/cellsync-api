using CellSync.Domain.Events.Messages;

namespace CellSync.Domain.Events.Config;

public interface IEventDispatcher
{
    public Task DispatchAsync(string eventType, IEventMessage message);

    public Type? GetMessageType(string? eventType);
}