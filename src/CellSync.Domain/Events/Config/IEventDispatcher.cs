using CellSync.Domain.Events.Messages;

namespace CellSync.Domain.Events.Config;

public interface IEventDispatcher
{
    public Task DispatchAsync(IEventMessage message);

    public Type? GetMessageType(string? messageTypeName);
}