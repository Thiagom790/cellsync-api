using CellSync.Consumer.EventProcessors;
using CellSync.Consumer.Helpers;
using CellSync.Consumer.Services;
using CellSync.Consumer.Settings;
using CellSync.Domain.Enums;
using Microsoft.Extensions.Configuration;

var env = EnvironmentHelpers.GetEnvironmentName();

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
    .Build();

var azureEventHubSettings = config.GetSection("AzureEventHubSettings").Get<AzureEventHubSettings>();

var consumer = new AzureEventHubConsumer(azureEventHubSettings!);

var addVisitorProcessor = new AddVisitorEventProcessor();

consumer.Subscribe(EventNames.ADD_VISITOR, addVisitorProcessor);

consumer.StartAsync().GetAwaiter().GetResult();