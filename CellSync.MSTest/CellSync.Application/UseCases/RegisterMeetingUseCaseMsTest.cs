using CellSync.Application.UseCases.Meeting.Register;
using CellSync.Domain.Entities;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Cell;
using CellSync.Domain.Repositories.Meeting;
using Moq;

namespace CellSync.MSTest.CellSync.Application.UseCases;

[TestClass]
public class RegisterMeetingUseCaseMsTest
{
    [TestMethod]
    public async Task ExecuteAsync_RegisterMeetingWithSuccess_ReturnMeetingId()
    {
        var meetingRepository = new Mock<IMeetingRepository>();
        var cellRepository = new Mock<ICellRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var useCase = new RegisterMeetingUseCase(meetingRepository.Object, cellRepository.Object, unitOfWork.Object);

        var cellId = Guid.NewGuid();

        var request = new RegisterMeetingRequest
        {
            MeetingDate = DateTime.UtcNow,
            MeetingAddress = "123 Main St",
            CellId = cellId,
            MeetingMembers = [new MeetingMemberRequest { MemberId = Guid.NewGuid() }]
        };

        var cell = new Cell
        {
            Id = cellId,
            Name = "Cell Name",
            IsActive = true,
            Address = "123 Main St",
            CurrentLeaderId = Guid.NewGuid(),
        };

        cellRepository.Setup(x => x.GetByIdAsync(cellId))
            .ReturnsAsync(cell);

        meetingRepository.Setup(x => x.AddAsync(It.IsAny<Meeting>()))
            .Returns(Task.CompletedTask);

        unitOfWork.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

        var response = await useCase.ExecuteAsync(request);

        Assert.IsInstanceOfType<Guid>(response.Id);

        cellRepository.Verify(x => x.GetByIdAsync(cellId), Times.Once);

        meetingRepository.Verify(m => m.AddAsync(It.Is<Meeting>(meeting =>
            meeting.MeetingDate == request.MeetingDate &&
            meeting.MeetingAddress == request.MeetingAddress &&
            meeting.CellId == cellId &&
            meeting.LeaderId == cell.CurrentLeaderId &&
            meeting.MeetingMembers.Count == request.MeetingMembers.Count
        )), Times.Once);

        unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
    }
}