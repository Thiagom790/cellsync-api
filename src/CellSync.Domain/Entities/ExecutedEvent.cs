namespace CellSync.Domain.Entities;

public class ExecutedEvent
{
    public long Id { get; set; }
    public string EventName {get; set;} = string.Empty;
    public string EventDescription {get; set;} = string.Empty;
    public DateTimeOffset EventTime {get; set;}
}