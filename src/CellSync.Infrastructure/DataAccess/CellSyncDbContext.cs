using CellSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess;

internal class CellSyncDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Member> Members { get; set; }

    public DbSet<Cell> Cells { get; set; }

    public DbSet<Meeting> Meetings { get; set; }

    public DbSet<MeetingMember> MeetingMembers { get; set; }

    public DbSet<CellLeaderHistory> CellsLeaderHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cell>()
            .HasMany(cell => cell.LeaderHistory)
            .WithMany(member => member.LeadCellHistory)
            .UsingEntity<CellLeaderHistory>(entity => entity
                .Property(props => props.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
            );

        modelBuilder.Entity<Member>()
            .HasMany(member => member.LedCells)
            .WithOne(cell => cell.CurrentLeader)
            .HasForeignKey(cell => cell.CurrentLeaderId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        modelBuilder.Entity<Member>()
            .HasMany(member => member.LedMeetings)
            .WithOne(meeting => meeting.Leader)
            .HasForeignKey(meeting => meeting.LeaderId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        modelBuilder.Entity<Meeting>()
            .HasMany(meeting => meeting.Members)
            .WithMany(member => member.ParticipatedMeetings)
            .UsingEntity<MeetingMember>();
    }
}