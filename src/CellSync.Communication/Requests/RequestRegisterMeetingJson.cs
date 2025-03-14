namespace CellSync.Communication.Requests;

public class RequestRegisterMeetingJson
{
    public DateTime MeetingDate { get; set; }
    public string MeetingAddress { get; set; } = string.Empty;
    public List<RequestRegisterMeetingMemberJson> MeetingMembers { get; set; } = [];
}

public class RequestRegisterMeetingMemberJson
{
    public Guid MemberId { get; set; }
    public bool IsLeader { get; set; }
}