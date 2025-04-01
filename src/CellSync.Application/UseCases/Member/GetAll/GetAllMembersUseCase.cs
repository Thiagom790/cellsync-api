using CellSync.Communication.Responses;
using CellSync.Domain.Repositories.Member;

namespace CellSync.Application.UseCases.Member.GetAll;

public class GetAllMembersUseCase(IMemberRepository memberRepository) : IGetAllMembersUseCase
{
    public async Task<ResponseGetAllMembersJson> ExecuteAsync()
    {
        var result = await memberRepository.GetAll();

        var response = new ResponseGetAllMembersJson
        {
            Members = result.Select(member => new ResponseMemberJson
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                ProfileType = member.ProfileType
            }).ToList()
        };

        return response;
    }
}