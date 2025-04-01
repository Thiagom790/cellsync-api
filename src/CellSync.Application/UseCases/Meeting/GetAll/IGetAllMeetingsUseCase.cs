using CellSync.Communication.Responses;

namespace CellSync.Application.UseCases.Meeting.GetAll;

public interface IGetAllMeetingsUseCase
{
    Task<ResponseGetAllMeetingsJson> ExecuteAsync();
}