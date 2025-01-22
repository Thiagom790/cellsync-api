using CellSync.Domain.Entities;

namespace CellSync.Domain.Repositories;

public interface IMemberRepository
{
    Task<Member> Add(Member member);

    Task<List<Member>> GetAll();
}