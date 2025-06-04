using System.Reflection;
using CellSync.Consumer.EventProcessors;
using CellSync.Consumer.Services;
using CellSync.Consumer.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CellSync.Consumer;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddEventConsumerServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Registra as configurações
        services.Configure<AzureEventHubSettings>(configuration.GetSection("AzureEventHubSettings"));
        services.AddSingleton<IEventHubSettings>(sp => 
            sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<AzureEventHubSettings>>().Value);
        
        // Registra o consumer
        services.AddSingleton<AzureEventHubConsumer>();
        
        // Descobre e registra todos os processadores de eventos
        RegisterEventProcessors(services);
        
        return services;
    }
    
    private static void RegisterEventProcessors(IServiceCollection services)
    {
        var processorTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => 
                !type.IsInterface && 
                !type.IsAbstract && 
                type.GetInterfaces().Any(i => 
                    i.IsGenericType && 
                    i.GetGenericTypeDefinition() == typeof(IEventProcessor<>)))
            .ToList();
        
        foreach (var processorType in processorTypes)
        {
            services.AddTransient(processorType);
        }
    }
}
