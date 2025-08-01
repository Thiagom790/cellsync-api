namespace CellSync.Application.UseCases.Meeting.Register;

public interface IRegisterMeetingUseCase
{
    Task<RegisterMeetingResponse> ExecuteAsync(RegisterMeetingRequest request);
}