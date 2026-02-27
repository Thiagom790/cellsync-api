using CellSync.Domain.Repositories.Member;
using Microsoft.Extensions.Logging;

namespace CellSync.Application.UseCases.Member.GetAll;

public class GetAllMembersUseCase(IMemberRepository memberRepository, ILogger<GetAllMembersUseCase> logger)
    : IGetAllMembersUseCase
{
    public async Task<GetAllMembersResponse> ExecuteAsync()
    {
        logger.LogInformation("Getting all members");
        var result = await memberRepository.GetAllAsync();

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