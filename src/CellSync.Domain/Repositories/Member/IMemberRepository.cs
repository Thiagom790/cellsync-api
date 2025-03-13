namespace CellSync.Domain.Repositories.Member;

public interface IMemberRepository
{
    Task Add(Entities.Member member);

    Task<List<Entities.Member>> GetAll();

    Task<Entities.Member?> GetById(Guid id);

    void Update(Entities.Member member);
}