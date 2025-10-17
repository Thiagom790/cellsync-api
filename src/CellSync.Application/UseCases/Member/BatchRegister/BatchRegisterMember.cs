using CellSync.Domain.Enums;

namespace CellSync.Application.UseCases.Member.BatchRegister;

public class BatchRegisterMember
{
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string ProfileType { get; set; } = ProfileTypes.MEMBER;
}