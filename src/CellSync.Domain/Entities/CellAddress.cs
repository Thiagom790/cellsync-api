namespace CellSync.Domain.Entities;

public class CellAddress
{
    public Guid Id { get; set; }
    public bool IsCurrent { get; set; }
    public string Address { get; set; } = string.Empty;
    public Guid CellId { get; set; }
    public Cell Cell { get; set; } = null!;
}