namespace CellSync.Domain.Entities;

public class Cell
{
    public Guid Id { get; init; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? Address { get; set; }
}