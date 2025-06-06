namespace CellSync.Application.UseCases.Meeting.Register;

public class RegisterMeetingRequest
{
    public DateTime MeetingDate { get; set; }
    public string MeetingAddress { get; set; } = string.Empty;
    public Guid CellId { get; set; }
    public List<MeetingMemberRequest> MeetingMembers { get; set; } = [];
}

public class MeetingMemberRequest
{
    public Guid MemberId { get; set; }
}