using CellSync.Communication.Requests;
using CellSync.Communication.Responses;
using CellSync.Domain.Entities;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Meeting;

namespace CellSync.Application.UseCases.Meeting.Register;

public class RegisterMeetingUseCase(IMeetingRepository repository, IUnitOfWork unitOfWork) : IRegisterMeetingUseCase
{
    public async Task<ResponseRegisterMeetingJson> ExecuteAsync(RequestRegisterMeetingJson request)
    {
        var meetingId = Guid.NewGuid();

        var meeting = new Domain.Entities.Meeting
        {
            Id = meetingId,
            MeetingDate = request.MeetingDate,
            MeetingAddress = request.MeetingAddress,
            CellId = request.CellId,
            MeetingMembers = request.MeetingMembers.Select(meetingMember => new MeetingMember
            {
                MeetingId = meetingId,
                MemberId = meetingMember.MemberId
            }).ToList(),
        };

        await repository.AddAsync(meeting);
        await unitOfWork.CommitAsync();

        return new ResponseRegisterMeetingJson { Id = meeting.Id };
    }
}