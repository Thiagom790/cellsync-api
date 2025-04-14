using CellSync.Communication.Requests;
using CellSync.Communication.Responses;
using CellSync.Domain.Entities;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Cell;
using CellSync.Domain.Repositories.Meeting;

namespace CellSync.Application.UseCases.Meeting.Register;

public class RegisterMeetingUseCase(
    IMeetingRepository meetingRepository,
    ICellRepository cellRepository,
    IUnitOfWork unitOfWork
) : IRegisterMeetingUseCase
{
    public async Task<ResponseRegisterMeetingJson> ExecuteAsync(RequestRegisterMeetingJson request)
    {
        var cell = await cellRepository.GetByIdAsync(request.CellId);

        if (cell is null)
        {
            throw new Exception("Cell not found");
        }

        var meetingId = Guid.NewGuid();

        var meeting = new Domain.Entities.Meeting
        {
            Id = meetingId,
            MeetingDate = request.MeetingDate,
            MeetingAddress = request.MeetingAddress,
            CellId = cell.Id,
            LeaderId = cell.CurrentLeaderId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            MeetingMembers = request.MeetingMembers.Select(meetingMember => new MeetingMember
            {
                MeetingId = meetingId,
                MemberId = meetingMember.MemberId
            }).ToList(),
        };

        await meetingRepository.AddAsync(meeting);
        await unitOfWork.CommitAsync();

        return new ResponseRegisterMeetingJson { Id = meeting.Id };
    }
}