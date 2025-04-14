namespace CellSync.Domain.Entities;

public class CellLeaderHistory
{
    public Guid CellId { get; set; }
    public Guid LeaderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Cell Cell { get; set; }
    public Member Leader { get; set; }
}