using CellSync.Communication.Responses;

namespace CellSync.Application.UseCases.Member.GetById;

public interface IGetMemberByIdUseCase
{
    Task<ResponseGetMemberByIdJson> ExecuteAsync(Guid id);
}