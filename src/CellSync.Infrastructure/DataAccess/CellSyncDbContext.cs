using CellSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess;

internal class CellSyncDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Member> Members { get; set; }

    public DbSet<Cell> Cells { get; set; }
}