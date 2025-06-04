namespace CellSync.Consumer.EventProcessors;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class EventProcessorAttribute(string eventName) : Attribute // AQUI CRIAMOS O ATRIBUTO PERSONALIZADO PARA MAPEAR OS EVENTOS
{
    public string EventName { get; } = eventName ?? throw new ArgumentNullException(nameof(eventName));
}
