using CellSync.Application.UseCases.Cell.GetAll;
using CellSync.Application.UseCases.Cell.GetById;
using CellSync.Application.UseCases.Cell.Register;
using CellSync.Application.UseCases.Member.GetAll;
using CellSync.Application.UseCases.Member.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CellSync.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterMemberUseCase, RegisterMemberUseCase>();
        services.AddScoped<IGetAllMembersUseCase, GetAllMembersUseCase>();
        services.AddScoped<IRegisterCellUseCase, RegisterCellUseCase>();
        services.AddScoped<IGetCellByIdUseCase, GetCellByIdUseCase>();
        services.AddScoped<IGetAllCellsUseCase, GetAllCellsUseCase>();
    }
}