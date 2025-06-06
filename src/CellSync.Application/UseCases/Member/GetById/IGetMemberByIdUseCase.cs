namespace CellSync.Application.UseCases.Member.GetById;

public interface IGetMemberByIdUseCase
{
    Task<GetMemberByIdResponse> ExecuteAsync(Guid id);
}