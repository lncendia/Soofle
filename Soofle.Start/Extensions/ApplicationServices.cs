using Soofle.Application.Abstractions.Elements.ServicesInterfaces;
using Soofle.Application.Abstractions.Links.ServicesInterfaces;
using Soofle.Application.Abstractions.Participants.ServicesInterfaces;
using Soofle.Application.Abstractions.Payments.ServicesInterfaces;
using Soofle.Application.Abstractions.Profile.ServicesInterfaces;
using Soofle.Application.Abstractions.Proxies.ServicesInterfaces;
using Soofle.Application.Abstractions.ReportsManagement.ServicesInterfaces;
using Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using Soofle.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using Soofle.Application.Abstractions.Users.ServicesInterfaces;
using Soofle.Application.Abstractions.Vk.ServicesInterfaces;
using Soofle.Application.Services.Elements;
using Soofle.Application.Services.Elements.Mappers;
using Soofle.Application.Services.Links;
using Soofle.Application.Services.Participants;
using Soofle.Application.Services.Payments;
using Soofle.Application.Services.Profile;
using Soofle.Application.Services.Proxies;
using Soofle.Application.Services.ReportsManagement;
using Soofle.Application.Services.ReportsProcessors.Initializers;
using Soofle.Application.Services.ReportsProcessors.Processors;
using Soofle.Application.Services.ReportsQuery;
using Soofle.Application.Services.ReportsQuery.Mappers;
using Soofle.Application.Services.Users;
using Soofle.Application.Services.Vk;

namespace Soofle.Start.Extensions;

internal static class ApplicationServices
{
    internal static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ILinkManager, LinkManager>();
        services.AddScoped<IParticipantManager, ParticipantManager>();
        services.AddScoped<IPaymentManager, PaymentManager>();
        services.AddScoped<IProxyManager, ProxyManager>();
        services.AddScoped<IProxyGetter, ProxyGetterService>();
        services.AddScoped<IReportMapper, ReportMapper>();
        services.AddScoped<IReportManager, ReportManager>();
        services.AddScoped<IReportElementManager, ElementManager>();
        services.AddScoped<IElementMapper, ElementMapper>();
        services.AddScoped<IReportCreationService, ReportCreationService>();
        services.AddScoped<IReportStarter, ReportStarterService>();
        services.AddScoped<IReportProcessorService, ReportProcessorService>();
        services.AddScoped<IReportInitializerService, ReportInitializerService>();
        services.AddScoped<IProfileService, UserProfileService>();
        services.AddScoped<IUserParametersService, UserParametersService>();
        services.AddScoped<IVkManager, VkManager>();
        services.AddScoped<IUserParticipantsService, UserParticipantsService>();
        services.AddScoped<IUserLinksService, UserLinksService>();
        services.AddScoped<IParticipantMapper, ParticipantMapper>();
        services.AddScoped<IUsersManager, UsersManager>();
    }
}