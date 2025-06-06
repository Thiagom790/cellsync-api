namespace CellSync.Application.UseCases.Cell.GetById;

public class GetCellByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public LeaderResponse? CurrentLeader { get; set; }
    public List<LeaderResponse> LeaderHistory { get; set; } = [];
}

public class LeaderResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}