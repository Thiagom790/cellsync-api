namespace CellSync.Domain.Entities;

public class Meeting
{
    public Guid Id { get; set; }
    public DateTime MeetingDate { get; set; }
    public string MeetingAddress { get; set; } = string.Empty;
    public Guid CellId { get; set; }
    public Guid? LeaderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Cell Cell { get; set; } = null!;
    public Member? Leader { get; set; }
    public List<Member> Members { get; set; } = [];
    public List<MeetingMember> MeetingMembers { get; set; } = [];
}