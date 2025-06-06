namespace CellSync.Application.UseCases.Cell.Register;

public class RegisterCellRequest
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string? Address { get; set; }
    public Guid? CurrentLeaderId { get; set; }
}