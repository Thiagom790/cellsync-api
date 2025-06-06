namespace CellSync.Application.UseCases.Member.GetAll;

public interface IGetAllMembersUseCase
{
    Task<GetAllMembersResponse> ExecuteAsync();
}