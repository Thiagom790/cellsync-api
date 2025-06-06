using CellSync.Domain.Events.Messages;

namespace CellSync.Domain.Events.Config;

public interface IEventMessageHandler
{
    public Task HandleAsync(IEventMessage eventData);

    public static abstract Type MessageType { get; }
}