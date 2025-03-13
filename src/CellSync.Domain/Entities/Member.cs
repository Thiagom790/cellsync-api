using CellSync.Domain.Enums;

namespace CellSync.Domain.Entities;

public class Member
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string ProfileType { get; init; } = ProfileTypes.MEMBER;
}