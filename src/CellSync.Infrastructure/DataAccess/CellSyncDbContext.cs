using CellSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess;

internal class CellSyncDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Member> Members { get; set; }

    public DbSet<Cell> Cells { get; set; }

    public DbSet<Meeting> Meetings { get; set; }

    public DbSet<MeetingMember> MeetingMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meeting>()
            .HasMany(e => e.Members)
            .WithMany(e => e.Meetings)
            .UsingEntity<MeetingMember>();
    }
}