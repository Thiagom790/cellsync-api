namespace CellSync.Communication.Requests;

public class RequestUpdateMemberJson
{
    public string Name { get; } = null!;
    public string? Email { get; } = null;
    public string? Phone { get; } = null;
    public string ProfileType { get; } = string.Empty;
}