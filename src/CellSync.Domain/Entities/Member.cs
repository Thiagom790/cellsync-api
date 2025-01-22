using CellSync.Domain.Enums;

namespace CellSync.Domain.Entities;

public class Member
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string ProfileType { get; set; } = ProfileTypes.MEMBER;
}