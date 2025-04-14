namespace CellSync.Communication.Requests;

public class RequestUpdateCellJson
{
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public string? Address { get; set; }
    public Guid? CurrentLeaderId { get; set; }
}