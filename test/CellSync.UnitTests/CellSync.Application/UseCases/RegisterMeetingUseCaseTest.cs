using CellSync.Application.UseCases.Meeting.Register;
using CellSync.Domain.Entities;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Cell;
using CellSync.Domain.Repositories.Meeting;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CellSync.UnitTests.CellSync.Application.UseCases;

public class RegisterMeetingUseCaseTest
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly ICellRepository _cellRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly RegisterMeetingUseCase _useCase;

    public RegisterMeetingUseCaseTest()
    {
        _meetingRepository = Substitute.For<IMeetingRepository>();
        _cellRepository = Substitute.For<ICellRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();

        _useCase = new RegisterMeetingUseCase(_meetingRepository, _cellRepository, _unitOfWork);
    }

    [Fact]
    public async Task ExecuteAsync_RegisterMeetingWithSuccess_ReturnMeetingId()
    {
        //Arrange
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

        _cellRepository.GetByIdAsync(cellId).Returns(cell);
        _meetingRepository.AddAsync(Arg.Any<Meeting>()).Returns(Task.CompletedTask);
        _unitOfWork.CommitAsync().Returns(Task.CompletedTask);

        //Act
        var response = await _useCase.ExecuteAsync(request);

        //Assert
        Assert.IsType<Guid>(response.Id);

        await _cellRepository.Received(1).GetByIdAsync(cellId);

        await _meetingRepository.Received(1).AddAsync(Arg.Is<Meeting>(m =>
            m.MeetingDate == request.MeetingDate &&
            m.MeetingAddress == request.MeetingAddress &&
            m.CellId == cellId &&
            m.LeaderId == cell.CurrentLeaderId &&
            m.MeetingMembers.Count == request.MeetingMembers.Count
        ));

        await _unitOfWork.Received(1).CommitAsync();
    }

    [Fact]
    public async Task ExecuteAsync_CellNotFound_ThrowsException()
    {
        // Arrange
        var request = new RegisterMeetingRequest
        {
            MeetingDate = DateTime.UtcNow,
            MeetingAddress = "123 Main St",
            CellId = Guid.NewGuid(),
            MeetingMembers = [new MeetingMemberRequest { MemberId = Guid.NewGuid() }]
        };

        _cellRepository.GetByIdAsync(request.CellId).ReturnsNull();

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(async () => await _useCase.ExecuteAsync(request));

        await _cellRepository.Received(1).GetByIdAsync(request.CellId);

        Assert.Equal("Cell not found", ex.Message);

        await _meetingRepository.DidNotReceive().AddAsync(Arg.Any<Meeting>());
        await _unitOfWork.DidNotReceive().CommitAsync();

        Assert.IsType<Exception>(ex);
    }
}