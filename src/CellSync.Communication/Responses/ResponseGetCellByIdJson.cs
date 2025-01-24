namespace CellSync.Communication.Responses;

public class ResponseGetCellByIdJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public ResponseGetCellAddressJson? CurrentAddress { get; set; }
}