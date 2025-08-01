namespace CellSync.Application.Events;

public interface IEventMessageHandlerMapProvider
{
    Dictionary<string, Type> GetMessageTypes();
}