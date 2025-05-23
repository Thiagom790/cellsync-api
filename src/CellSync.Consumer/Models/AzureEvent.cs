namespace CellSync.Consumer.Models;

public class AzureEvent<TEventData>
{
    public string? EventName { get; set; }
    public TEventData? EventData { get; set; }
}