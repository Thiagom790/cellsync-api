namespace CellSync.Application.UseCases.Meeting.GetAll;

public class GetAllMeetingsResponse
{
    public List<MeetingsResponse> Meetings { get; set; } = [];
}

public class MeetingsResponse
{
    public Guid Id { get; set; }
    public DateTime MeetingDate { get; set; }
    public string MeetingAddress { get; set; } = string.Empty;
    public Guid CellId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public MeetingsMemberResponse? Leader { get; set; }
    public List<MeetingsMemberResponse> Members { get; set; } = [];
}

public class MeetingsMemberResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}