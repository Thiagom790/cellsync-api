namespace CellSync.Domain.Entities;

public class Cell
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public ICollection<CellAddress> Addresses { get; set; } = [];
}