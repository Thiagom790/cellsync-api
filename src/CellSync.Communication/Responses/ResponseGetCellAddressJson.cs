namespace CellSync.Communication.Responses;

public class ResponseGetCellAddressJson
{
    public Guid Id { get; set; }
    public string Address { get; set; } = string.Empty;
}