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
    public async Task<RegisterMeetingResponse> ExecuteAsync(RegisterMeetingRequest registerMeetingRequest)
    {
        var cell = await cellRepository.GetByIdAsync(registerMeetingRequest.CellId);

        if (cell is null)
        {
            throw new Exception("Cell not found");
        }

        var meetingId = Guid.NewGuid();

        var meeting = new Domain.Entities.Meeting
        {
            Id = meetingId,
            MeetingDate = registerMeetingRequest.MeetingDate,
            MeetingAddress = registerMeetingRequest.MeetingAddress,
            CellId = cell.Id,
            LeaderId = cell.CurrentLeaderId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            MeetingMembers = registerMeetingRequest.MeetingMembers.Select(meetingMember => new MeetingMember
            {
                MeetingId = meetingId,
                MemberId = meetingMember.MemberId
            }).ToList(),
        };

        await meetingRepository.AddAsync(meeting);
        await unitOfWork.CommitAsync();

        return new RegisterMeetingResponse { Id = meeting.Id };
    }
}