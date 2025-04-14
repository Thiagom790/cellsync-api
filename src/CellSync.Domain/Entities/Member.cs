using CellSync.Domain.Enums;

namespace CellSync.Domain.Entities;

public class Member
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string ProfileType { get; set; } = ProfileTypes.MEMBER;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Cell> LedCells { get; set; } = [];
    public List<Cell> LeadCellHistory { get; set; } = [];
    public List<Meeting> LedMeetings { get; set; } = [];
    public List<Meeting> ParticipatedMeetings { get; set; } = [];
}