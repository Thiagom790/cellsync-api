﻿namespace CellSync.Communication.Requests;

public class RequestRegisterCellJson
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string? Address { get; set; } = null;
}