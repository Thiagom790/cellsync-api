namespace CellSync.Application.UseCases.Cell.GetAll;

public class GetAllCellsResponse
{
    public List<CellResponse> Cells { get; set; } = [];
}

public class CellResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? Address { get; set; }
    public Guid? CurrentLeaderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}