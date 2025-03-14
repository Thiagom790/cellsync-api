using CellSync.Communication.Requests;
using CellSync.Communication.Responses;

namespace CellSync.Application.UseCases.Meeting.Register;

public interface IRegisterMeetingUseCase
{
    Task<ResponseRegisterMeetingJson> ExecuteAsync(RequestRegisterMeetingJson request);
}