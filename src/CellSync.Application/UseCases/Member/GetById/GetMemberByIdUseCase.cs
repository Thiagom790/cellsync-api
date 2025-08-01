using CellSync.Domain.Repositories.Member;

namespace CellSync.Application.UseCases.Member.GetById;

public class GetMemberByIdUseCase(IMemberRepository repository) : IGetMemberByIdUseCase
{
    public async Task<GetMemberByIdResponse> ExecuteAsync(Guid id)
    {
        var result = await repository.GetByIdAsync(id);

        if (result is null)
        {
            throw new Exception("Member not found");
        }

        var response = new GetMemberByIdResponse
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email,
            Phone = result.Phone,
            ProfileType = result.ProfileType,
            CreatedAt = result.CreatedAt,
            UpdatedAt = result.UpdatedAt,
        };

        return response;
    }
}