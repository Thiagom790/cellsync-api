using System.Reflection;

namespace CellSync.Consumer.EventProcessors;

public class EventProcessorManager(IServiceProvider serviceProvider)
{
    public Dictionary<string, Type> DiscoverEventProcessors()
    {
        var eventProcessorsMap = new Dictionary<string, Type>();

        // Busca todos os tipos que implementam IEventProcessor<>
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
            // Obtém o atributo EventProcessor PARA DESCOBRIR QUAL O TIPO SERA PROCESSADO
            var attribute = processorType.GetCustomAttribute<EventProcessorAttribute>();
            if (attribute != null)
            {
                eventProcessorsMap[attribute.EventName] = processorType;
            }
        }

        return eventProcessorsMap;
    }

    public object GetProcessorInstance(Type processorType)
        => serviceProvider.GetService(processorType) ??
           Activator.CreateInstance(processorType)!;


    public Type GetEventDataType(Type processorType)
        => processorType.GetInterfaces()
            .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventProcessor<>))
            .GetGenericArguments()[0];
}