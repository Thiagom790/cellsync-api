using CellSync.Communication.Responses;

namespace CellSync.Application.UseCases.Member.GetAll;

public interface IGetAllMembersUseCase
{
    Task<ResponseGetAllMembersJson> Execute();
}