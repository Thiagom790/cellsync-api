namespace CellSync.Communication.Requests;

public class RequestRegisterMemberJson
{
    public string Name { get; set; } = null!;
    public string? Email { get; set; } = null;
    public string? Phone { get; set; } = null;
    public string ProfileType { get; set; } = string.Empty;
}