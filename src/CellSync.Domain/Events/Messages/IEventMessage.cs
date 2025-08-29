namespace CellSync.Domain.Events.Messages;

public interface IEventMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; }
}