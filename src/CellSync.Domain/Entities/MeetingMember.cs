namespace CellSync.Domain.Entities;

public class MeetingMember
{
    public Guid MeetingId { get; init; }
    public Guid MemberId { get; init; }
    public bool IsLeader { get; init; }
    public Meeting Meeting { get; init; } = null!;
    public Member Member { get; init; } = null!;
}