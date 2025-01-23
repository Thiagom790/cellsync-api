namespace CellSync.Domain.Repositories.Member;

public interface IMemberRepository
{
    Task<Entities.Member> Add(Entities.Member member);

    Task<List<Entities.Member>> GetAll();
}