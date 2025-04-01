using CellSync.Communication.Responses;
using CellSync.Domain.Repositories.Meeting;

namespace CellSync.Application.UseCases.Meeting.GetAll;

public class GetAllMeetingsUseCase(IMeetingRepository meetingRepository) : IGetAllMeetingsUseCase
{
    public async Task<ResponseGetAllMeetingsJson> ExecuteAsync()
    {
        var result = await meetingRepository.GetAllAsync();

        var response = new ResponseGetAllMeetingsJson
        {
            Meetings = result.Select(meeting => new MeetingsJson
            {
                Id = meeting.Id,
                MeetingDate = meeting.MeetingDate,
                CellId = meeting.CellId,
                MeetingAddress = meeting.MeetingAddress,
            }).ToList()
        };

        return response;
    }
}