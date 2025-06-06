namespace CellSync.Application.UseCases.Cell.Update;

public class UpdateCellRequest
{
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public string? Address { get; set; }
    public Guid? CurrentLeaderId { get; set; }
}