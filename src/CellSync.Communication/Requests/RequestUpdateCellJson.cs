namespace CellSync.Communication.Requests;

public class RequestUpdateCellJson
{
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? Address { get; set; }
}