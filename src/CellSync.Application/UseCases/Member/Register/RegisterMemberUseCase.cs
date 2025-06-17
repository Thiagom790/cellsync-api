using CellSync.Domain.Enums;
using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Member;

namespace CellSync.Application.UseCases.Member.Register;

public class RegisterMemberUseCase(
    IMemberRepository memberRepository,
    IUnitOfWork unitOfWork,
    IEventPublisher eventPublisher
) : IRegisterMemberUseCase
{
    public async Task<RegisterMemberResponse> Execute(RegisterMemberRequest request)
    {
        var newMember = new Domain.Entities.Member
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            Phone = request.Phone,
            ProfileType = request.ProfileType,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await memberRepository.Add(newMember);
        await unitOfWork.CommitAsync();

        if (newMember.ProfileType == ProfileTypes.VISITOR)
        {
            await eventPublisher.PublishAsync(
                EventNames.REGISTER_VISITOR,
                new RegisterVisitorEventMessage
                {
                    Email = newMember.Email,
                    Name = newMember.Name,
                    Phone = newMember.Phone,
                    Id = newMember.Id,
                });
        }

        return new RegisterMemberResponse { Id = newMember.Id };
    }
}