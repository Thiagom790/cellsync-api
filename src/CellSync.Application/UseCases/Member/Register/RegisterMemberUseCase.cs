using CellSync.Communication.Requests;
using CellSync.Communication.Responses;
using CellSync.Domain.Repositories.Member;

namespace CellSync.Application.UseCases.Member.Register;

public class RegisterMemberUseCase(IMemberRepository memberRepository) : IRegisterMemberUseCase
{
    private readonly IMemberRepository _memberRepository = memberRepository;

    public async Task<ResponseRegisterMemberJson> Execute(RequestRegisterMemberJson request)
    {
        var newMember = new Domain.Entities.Member
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            Phone = request.Phone,
            ProfileType = request.ProfileType
        };

        await _memberRepository.Add(newMember);
        return new ResponseRegisterMemberJson { Id = newMember.Id };
    }
}