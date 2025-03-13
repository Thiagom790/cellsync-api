namespace CellSync.Communication.Requests;

public class RequestRegisterCellJson
{
    public string Name { get; } = string.Empty;
    public bool IsActive { get; } = true;
    public string? Address { get; } = null;
}