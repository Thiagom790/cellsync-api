namespace CellSync.Domain.Entities;

public class Meeting
{
    public Guid Id { get; init; }
    public DateTime MeetingDate { get; init; }
    public string MeetingAddress { get; init; } = string.Empty;
    public Guid CellId { get; init; }
    public Cell Cell { get; init; } = null!;
    public List<Member> Members { get; init; } = [];
}