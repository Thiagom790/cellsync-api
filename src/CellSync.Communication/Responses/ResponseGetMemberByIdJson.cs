namespace CellSync.Communication.Responses;

public class ResponseGetMemberByIdJson
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string ProfileType { get; init; } = string.Empty;
}