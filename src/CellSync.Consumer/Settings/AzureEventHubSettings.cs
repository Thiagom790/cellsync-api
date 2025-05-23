namespace CellSync.Consumer.Settings;

public class AzureEventHubSettings : IEventHubSettings
{
    public string EventHubConnectionString { get; set; } = string.Empty;
    public string EventHubName { get; set; } = string.Empty;
    public string StorageConnectionString { get; set; } = string.Empty;
    public string StorageContainerName { get; set; } = string.Empty;
}