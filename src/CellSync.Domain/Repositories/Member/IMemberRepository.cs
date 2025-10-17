namespace CellSync.Domain.Repositories.Member;

public interface IMemberRepository
{
    Task AddAsync(Entities.Member member);

    Task AddRangeAsync(IEnumerable<Entities.Member> members);

    Task<List<Entities.Member>> GetAllAsync();

    Task<Entities.Member?> GetByIdAsync(Guid id);

    void UpdateAsync(Entities.Member member);
}