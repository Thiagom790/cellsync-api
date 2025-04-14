using CellSync.Communication.Requests;
using CellSync.Communication.Responses;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Member;

namespace CellSync.Application.UseCases.Member.Register;

public class RegisterMemberUseCase(
    IMemberRepository memberRepository,
    IUnitOfWork unitOfWork
) : IRegisterMemberUseCase
{
    public async Task<ResponseRegisterMemberJson> Execute(RequestRegisterMemberJson request)
    {
        var newMember = new Domain.Entities.Member
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            Phone = request.Phone,
            ProfileType = request.ProfileType,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await memberRepository.Add(newMember);
        await unitOfWork.CommitAsync();

        return new ResponseRegisterMemberJson { Id = newMember.Id };
    }
}