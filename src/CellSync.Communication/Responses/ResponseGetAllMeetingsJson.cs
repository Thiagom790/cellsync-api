namespace CellSync.Communication.Responses;

public class ResponseGetAllMeetingsJson
{
    public List<MeetingsJson> Meetings { get; set; } = [];
}

public class MeetingsJson
{
    public Guid Id { get; set; }
    public DateTime MeetingDate { get; init; }
    public string MeetingAddress { get; init; } = string.Empty;
    public Guid CellId { get; init; }
}