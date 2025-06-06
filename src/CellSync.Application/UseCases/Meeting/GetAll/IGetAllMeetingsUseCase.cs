namespace CellSync.Application.UseCases.Meeting.GetAll;

public interface IGetAllMeetingsUseCase
{
    Task<GetAllMeetingsResponse> ExecuteAsync();
}