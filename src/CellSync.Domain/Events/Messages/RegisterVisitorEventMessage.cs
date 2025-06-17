namespace CellSync.Domain.Events.Messages;

public class RegisterVisitorEventMessage : IEventMessage
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}