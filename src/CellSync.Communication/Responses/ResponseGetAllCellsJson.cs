namespace CellSync.Communication.Responses;

public class ResponseGetAllCellsJson
{
    public List<ResponseCellJson> Cells { get; init; } = [];
}

public class ResponseCellJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? Address { get; set; }
}