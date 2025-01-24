namespace CellSync.Domain.Repositories.Member;

public interface IMemberRepository
{
    Task Add(Entities.Member member);

    Task<List<Entities.Member>> GetAll();
}