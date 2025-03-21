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
        var meeting = new Domain.Entities.Meeting
        {
            Id = Guid.NewGuid(),
            MeetingDate = request.MeetingDate,
            MeetingAddress = request.MeetingAddress,
            CellId = request.CellId,
        };

        var meetingMembers = request.MeetingMembers
            .Select(requestMember => new MeetingMember
            {
                MeetingId = meeting.Id,
                MemberId = requestMember.MemberId,
            })
            .ToList();

        await repository.AddAsync(meeting);
        await repository.AddMemberInMeetingAsync(meetingMembers);
        await unitOfWork.CommitAsync();

        return new ResponseRegisterMeetingJson { Id = meeting.Id };
    }
}