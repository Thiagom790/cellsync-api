namespace CellSync.Application.UseCases.Member.Register;

public interface IRegisterMemberUseCase
{
    Task<RegisterMemberResponse> ExecuteAsync(RegisterMemberRequest request);
}