namespace CellSync.Domain.Entities;

public class Meeting
{
    public Guid Id { get; init; }
    public DateTime MeetingDate { get; init; }
    public string MeetingAddress { get; init; } = string.Empty;
    public List<Member> Members { get; init; } = [];
}