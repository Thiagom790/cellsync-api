namespace CellSync.Domain.Events.Config;

[AttributeUsage(AttributeTargets.Class)]
public class EventMessageHandleAttribute(string eventName) : Attribute
{
    public string EventName { get; } = eventName ?? throw new ArgumentNullException(nameof(eventName));
}