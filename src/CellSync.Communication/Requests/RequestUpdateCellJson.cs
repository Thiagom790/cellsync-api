namespace CellSync.Communication.Requests;

public class RequestUpdateCellJson
{
    public string Name { get; } = null!;
    public bool IsActive { get; } = true;
    public string? Address { get; } = null;
}