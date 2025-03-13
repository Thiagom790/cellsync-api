using CellSync.Communication.Requests;

namespace CellSync.Application.UseCases.Member.Update;

public interface IUpdateMemberUseCase
{
    Task ExecuteAsync(Guid memberId, RequestUpdateMemberJson request);
}