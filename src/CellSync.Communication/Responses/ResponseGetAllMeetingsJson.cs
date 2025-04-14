namespace CellSync.Communication.Responses;

public class ResponseGetAllMeetingsJson
{
    public List<MeetingsJson> Meetings { get; set; } = [];
}

public class MeetingsJson
{
    public Guid Id { get; set; }
    public DateTime MeetingDate { get; set; }
    public string MeetingAddress { get; set; } = string.Empty;
    public Guid CellId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public MeetingsMemberJson? Leader { get; set; }
    public List<MeetingsMemberJson> Members { get; set; } = [];
}

public class MeetingsMemberJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}