using System.Reflection;
using CellSync.Domain.Events.Config;

namespace CellSync.Application.Events;

public class EventMessageHandlerMapProvider : IEventMessageHandlerMapProvider
{
    public Dictionary<string, Type> GetMessageTypes()
    {
        var handlerInterface = typeof(IEventMessageHandler<>);

        var handlerClasses = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type =>
                type is { IsClass: true, IsAbstract: false } &&
                type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            )
            .ToList();

        var messageTypes = handlerClasses
            .Select(type => new
            {
                EventName = type.GetCustomAttribute<EventMessageHandleAttribute>()?.EventName,
                MessageType = type.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
                    .GetGenericArguments()
                    .FirstOrDefault()
            })
            .Where(x => x.EventName is not null && x.MessageType is not null)
            .ToDictionary(x => x.EventName!, x => x.MessageType!);

        return messageTypes;
    }
}