using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RCPS.Core.DTOs;
using RCPS.Core.Validators;
using RCPS.Services.Implementations;
using RCPS.Services.Interfaces;
using RCPS.Services.Mappings;

namespace RCPS.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IChangeRequestService, ChangeRequestService>();
        services.AddScoped<IEffortService, EffortService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IReminderService, ReminderService>();
        services.AddScoped<IRevenueRecognitionService, RevenueRecognitionService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<ITeamMemberService, TeamMemberService>();

        services.AddAutoMapper(typeof(DomainMappingProfile).Assembly);

        services.AddScoped<IValidator<CreateProjectRequest>, CreateProjectRequestValidator>();
        services.AddScoped<IValidator<CreateChangeRequestRequest>, CreateChangeRequestRequestValidator>();
        services.AddScoped<IValidator<CreateEffortEntryRequest>, CreateEffortEntryRequestValidator>();
        services.AddScoped<IValidator<CreateInvoiceRequest>, CreateInvoiceRequestValidator>();
        services.AddScoped<IValidator<CreateReminderRequest>, CreateReminderRequestValidator>();
        services.AddScoped<IValidator<CreateRevenueRecognitionRequest>, CreateRevenueRecognitionRequestValidator>();

        return services;
    }
}
