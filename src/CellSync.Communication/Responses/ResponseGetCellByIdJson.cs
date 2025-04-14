namespace CellSync.Communication.Responses;

public class ResponseGetCellByIdJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ResponseLeaderJson? CurrentLeader { get; set; }
    public List<ResponseLeaderJson> LeaderHistory { get; set; } = [];
}

public class ResponseLeaderJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}