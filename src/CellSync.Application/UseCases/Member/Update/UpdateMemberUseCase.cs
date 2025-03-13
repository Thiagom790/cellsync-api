using CellSync.Communication.Requests;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Member;

namespace CellSync.Application.UseCases.Member.Update;

public class UpdateMemberUseCase(IMemberRepository repository, IUnitOfWork unitOfWork) : IUpdateMemberUseCase
{
    public async Task ExecuteAsync(Guid memberId, RequestUpdateMemberJson request)
    {
        var member = await repository.GetById(memberId);

        if (member is null)
        {
            throw new Exception($"Member with id {memberId} not found");
        }

        member.Name = request.Name;
        member.Email = request.Email;
        member.Phone = request.Phone;
        member.ProfileType = request.ProfileType;

        repository.Update(member);
        await unitOfWork.CommitAsync();
    }
}