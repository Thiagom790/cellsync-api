using CellSync.Communication.Responses;
using CellSync.Domain.Repositories.Member;

namespace CellSync.Application.UseCases.Member.GetById;

public class GetMemberByIdUseCase(IMemberRepository repository) : IGetMemberByIdUseCase
{
    public async Task<ResponseGetMemberByIdJson> ExecuteAsync(Guid id)
    {
        var result = await repository.GetById(id);

        if (result is null)
        {
            throw new Exception("Member not found");
        }

        var response = new ResponseGetMemberByIdJson
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email,
            Phone = result.Phone,
            ProfileType = result.ProfileType
        };

        return response;
    }
}