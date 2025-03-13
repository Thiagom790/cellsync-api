namespace CellSync.Communication.Responses;

public class ResponseGetCellByIdJson
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public bool IsActive { get; init; }
    public string? Address { get; init; }
}