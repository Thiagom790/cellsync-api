namespace CellSync.Domain.Entities;

public class MeetingMember
{
    public Guid MeetingId { get; set; }
    public Guid MemberId { get; set; }
}