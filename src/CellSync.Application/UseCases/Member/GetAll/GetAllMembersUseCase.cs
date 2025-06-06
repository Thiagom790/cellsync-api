using CellSync.Domain.Repositories.Member;

namespace CellSync.Application.UseCases.Member.GetAll;

public class GetAllMembersUseCase(IMemberRepository memberRepository) : IGetAllMembersUseCase
{
    public async Task<GetAllMembersResponse> ExecuteAsync()
    {
        var result = await memberRepository.GetAll();

        var response = new GetAllMembersResponse
        {
            Members = result.Select(member => new MemberResponse()
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                ProfileType = member.ProfileType,
                CreatedAt = member.CreatedAt,
                UpdatedAt = member.UpdatedAt,
            }).ToList()
        };

        return response;
    }
}