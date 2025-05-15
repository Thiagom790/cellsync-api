using CellSync.Communication.Requests;
using CellSync.Communication.Responses;

namespace CellSync.Application.UseCases.Member.Register;

public interface IRegisterMemberUseCase
{
    Task<RegisterMemberResponse> Execute(RegisterMemberRequest request);
}