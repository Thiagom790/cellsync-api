using CellSync.Application.UseCases.Member.Register;
using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Member;
using NSubstitute;

namespace CellSync.UnitTests.CellSync.Application.UseCases.Member.Register;

public class RegisterMemberUseCaseTest
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly RegisterMemberUseCase _useCase;

    public RegisterMemberUseCaseTest()
    {
        _memberRepository = Substitute.For<IMemberRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _eventPublisher = Substitute.For<IEventPublisher>();

        _useCase = new RegisterMemberUseCase(_memberRepository, _unitOfWork, _eventPublisher);
    }

    [Fact]
    public async Task ExecuteAsync_RegisterMemberWithSuccess_ReturnMemberId()
    {
        var request = new RegisterMemberRequest
        {
            Email = "teste.email@teste.com",
            Name = "Test Member",
            Phone = "1234567890",
            ProfileType = "visitor"
        };

        _memberRepository.AddAsync(Arg.Any<Domain.Entities.Member>()).Returns(Task.CompletedTask);

        _unitOfWork.CommitAsync().Returns(Task.CompletedTask);

        _eventPublisher.PublishAsync(Arg.Any<RegisterVisitorEventMessage>()).Returns(Task.CompletedTask);

        var response = await _useCase.ExecuteAsync(request);

        Assert.NotEqual(Guid.Empty, response.Id);

        await _memberRepository.Received(1).AddAsync(Arg.Is<Domain.Entities.Member>(arg =>
            arg.Email == request.Email &&
            arg.Name == request.Name &&
            arg.Phone == request.Phone &&
            arg.ProfileType == request.ProfileType
        ));

        await _unitOfWork.Received(1).CommitAsync();

        await _eventPublisher.Received(1).PublishAsync(Arg.Is<RegisterVisitorEventMessage>(e =>
            e.Email == request.Email &&
            e.Name == request.Name &&
            e.Phone == request.Phone &&
            e.Id == response.Id
        ));
    }
}