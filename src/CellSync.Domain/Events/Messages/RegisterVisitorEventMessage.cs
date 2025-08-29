using CellSync.Domain.Enums;

namespace CellSync.Domain.Events.Messages;

public class RegisterVisitorEventMessage : IEventMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = EventNames.REGISTER_VISITOR;
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}