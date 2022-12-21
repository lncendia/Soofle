using VkQ.Application.Abstractions.Links.ServicesInterfaces;
using VkQ.Application.Abstractions.Participants.ServicesInterfaces;
using VkQ.Application.Abstractions.Payments.ServicesInterfaces;
using VkQ.Application.Abstractions.Proxies.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.ReportProcessing;
using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.ServicesInterfaces.Manage;
using VkQ.Application.Abstractions.Vk.ServicesInterfaces;
using VkQ.Application.Services.Links;
using VkQ.Application.Services.Participants;
using VkQ.Application.Services.Payments;
using VkQ.Application.Services.Proxy;
using VkQ.Application.Services.ReportsManagement;
using VkQ.Application.Services.ReportsProcessors.Initializers;
using VkQ.Application.Services.ReportsProcessors.Processors;
using VkQ.Application.Services.ReportsQuery;
using VkQ.Application.Services.ReportsQuery.Mappers;
using VkQ.Application.Services.Users.Manage;
using VkQ.Application.Services.Vk;

namespace VkQ.Start.Extensions;

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
    }
}