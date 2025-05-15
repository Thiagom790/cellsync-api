namespace CellSync.Application.UseCases.Member.Register;

public class RegisterMemberRequest
{
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string ProfileType { get; set; } = string.Empty;
}