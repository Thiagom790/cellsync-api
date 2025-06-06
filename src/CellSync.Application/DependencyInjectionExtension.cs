using CellSync.Application.Events;
using CellSync.Application.UseCases.Cell.GetAll;
using CellSync.Application.UseCases.Cell.GetById;
using CellSync.Application.UseCases.Cell.Register;
using CellSync.Application.UseCases.Cell.Update;
using CellSync.Application.UseCases.Meeting.GetAll;
using CellSync.Application.UseCases.Meeting.Register;
using CellSync.Application.UseCases.Member.GetAll;
using CellSync.Application.UseCases.Member.GetById;
using CellSync.Application.UseCases.Member.Register;
using CellSync.Application.UseCases.Member.Update;
using CellSync.Domain.Events.Config;
using Microsoft.Extensions.DependencyInjection;

namespace CellSync.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection service)
    {
        AddUseCase(service);
        AddEventHandlers(service);
    }

    private static void AddUseCase(IServiceCollection service)
    {
        service.AddScoped<IRegisterMemberUseCase, RegisterMemberUseCase>();
        service.AddScoped<IGetAllMembersUseCase, GetAllMembersUseCase>();
        service.AddScoped<IRegisterCellUseCase, RegisterCellUseCase>();
        service.AddScoped<IGetCellByIdUseCase, GetCellByIdUseCase>();
        service.AddScoped<IGetAllCellsUseCase, GetAllCellsUseCase>();
        service.AddScoped<IUpdateCellUseCase, UpdateCellUseCase>();
        service.AddScoped<IUpdateMemberUseCase, UpdateMemberUseCase>();
        service.AddScoped<IGetMemberByIdUseCase, GetMemberByIdUseCase>();
        service.AddScoped<IRegisterMeetingUseCase, RegisterMeetingUseCase>();
        service.AddScoped<IGetAllMeetingsUseCase, GetAllMeetingsUseCase>();
    }

    private static void AddEventHandlers(IServiceCollection service)
    {
        service.AddScoped<RegisterVisitorMessageHandler>();
        service.AddSingleton<IEventDispatcher, EventDispatcher>();
    }
}