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
            Meetings = result.Select(meeting =>
            {
                var response = new MeetingsJson
                {
                    Id = meeting.Id,
                    MeetingDate = meeting.MeetingDate,
                    CellId = meeting.CellId,
                    MeetingAddress = meeting.MeetingAddress,
                    CreatedAt = meeting.CreatedAt,
                    UpdatedAt = meeting.UpdatedAt,
                };

                if (meeting.Members.Count > 0)
                {
                    response.Members = meeting.Members.Select(member => new MeetingsMemberJson
                    {
                        Id = member.Id,
                        Name = member.Name,
                    }).ToList();
                }

                if (meeting.Leader is not null)
                {
                    response.Leader = new MeetingsMemberJson
                    {
                        Id = meeting.Leader.Id,
                        Name = meeting.Leader.Name,
                    };
                }

                return response;
            }).ToList()
        };

        return response;
    }
}