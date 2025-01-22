using CellSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess;

public class CellSyncDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Member> Members { get; set; }
}