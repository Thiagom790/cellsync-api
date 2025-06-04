using CellSync.Consumer;
using CellSync.Consumer.Helpers;
using CellSync.Consumer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, config) =>
    {
        var env = EnvironmentHelpers.GetEnvironmentName();
        
        config.SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddEventConsumerServices(hostContext.Configuration);
    })
    .Build();

var consumer = host.Services.GetRequiredService<AzureEventHubConsumer>();
await consumer.StartAsync();

