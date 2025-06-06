namespace CellSync.Application.UseCases.Member.Update;

public class UpdateMemberRequest
{
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string ProfileType { get; set; } = string.Empty;
}