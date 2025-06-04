namespace CellSync.Consumer.Models;

public class AzureEvent<T>
{
    public string EventName { get; set; } = string.Empty;
    public T? EventData { get; set; }
}
