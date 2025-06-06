namespace CellSync.Application.UseCases.Member.GetAll;

public class GetAllMembersResponse
{
    public List<MemberResponse> Members { get; set; } = [];
}

public class MemberResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string ProfileType { get; set; } = null!;
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}