namespace CellSync.Communication.Responses;

public class ResponseGetAllCellsJson
{
    public List<ResponseCellJson> Cells { get; set; } = [];
}

public class ResponseCellJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? Address { get; set; }
    public Guid? CurrentLeaderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}