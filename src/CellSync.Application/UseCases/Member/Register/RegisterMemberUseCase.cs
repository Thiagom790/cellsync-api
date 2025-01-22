using CellSync.Communication.Requests;
using CellSync.Communication.Responses;
using CellSync.Domain.Repositories;
using CellSync.Domain.Entities;

namespace CellSync.Application.UseCases.Member.Register;

public class RegisterMemberUseCase(IMemberRepository memberRepository) : IRegisterMemberUseCase
{
    private readonly IMemberRepository _memberRepository = memberRepository;

    public async Task<ResponseRegisterMemberJson> Execute(RequestRegisterMemberJson request)
    {
        var newMember = new Domain.Entities.Member
        {
            Email = request.Email,
            Name = request.Name,
            Phone = request.Phone,
            ProfileType = request.ProfileType,
        };

        var addedMember = await _memberRepository.Add(newMember);
        var response = new ResponseRegisterMemberJson { Id = addedMember.Id };
        
        return response;
    }
}