﻿using CellSync.Communication.Responses;

namespace CellSync.Application.UseCases.Cell.GetById;

public interface IGetCellByIdUseCase
{
    Task<ResponseGetCellByIdJson?> Execute(Guid id);
}