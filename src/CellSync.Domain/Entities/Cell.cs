namespace CellSync.Domain.Entities;

public class Cell
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? Address { get; set; }
    public Guid? CurrentLeaderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Member? CurrentLeader { get; set; }
    public List<Member> LeaderHistory { get; set; } = [];
    public List<CellLeaderHistory> CellLeaderHistory { get; set; } = [];
}