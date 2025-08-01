using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Member;

namespace CellSync.Application.UseCases.Member.Update;

public class UpdateMemberUseCase(IMemberRepository repository, IUnitOfWork unitOfWork) : IUpdateMemberUseCase
{
    public async Task ExecuteAsync(Guid memberId, UpdateMemberRequest updateMemberRequest)
    {
        var member = await repository.GetByIdAsync(memberId);

        if (member is null)
        {
            throw new Exception($"Member with id {memberId} not found");
        }

        member.Name = updateMemberRequest.Name;
        member.Email = updateMemberRequest.Email;
        member.Phone = updateMemberRequest.Phone;
        member.ProfileType = updateMemberRequest.ProfileType;
        member.UpdatedAt = DateTime.UtcNow;

        repository.UpdateAsync(member);
        await unitOfWork.CommitAsync();
    }
}