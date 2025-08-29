namespace CellSync.Domain.Events.Config;

[AttributeUsage(AttributeTargets.Class)]
public class EventMessageHandleAttribute(string eventType) : Attribute
{
    public string EventName { get; } = eventType ?? throw new ArgumentNullException(nameof(eventType));
}