namespace CellSync.Domain.Events.Messages;

public interface IEventMessage
{
    public string MessageType { get; }
}