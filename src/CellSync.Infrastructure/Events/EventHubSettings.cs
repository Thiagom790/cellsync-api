namespace CellSync.Infrastructure.Events;

public class EventHubSettings : IEventHubSettings
{
    public string EventHubConnectionString { get; set; }
    public string EventHubName { get; set; }
    public string StorageConnectionString { get; set; }
    public string StorageContainerName { get; set; }
}