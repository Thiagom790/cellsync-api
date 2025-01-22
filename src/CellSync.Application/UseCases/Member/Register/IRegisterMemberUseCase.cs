using CellSync.Communication.Requests;
using CellSync.Communication.Responses;

namespace CellSync.Application.UseCases.Member.Register;

public interface IRegisterMemberUseCase
{
    Task<ResponseRegisterMemberJson> Execute(RequestRegisterMemberJson request);
}