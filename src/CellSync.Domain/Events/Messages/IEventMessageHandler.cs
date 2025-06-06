namespace CellSync.Domain.Events.Messages;

public interface IEventMessageHandler
{
    public Task HandleAsync(IEventMessage eventData);

    public static abstract Type MessageType { get; }
}