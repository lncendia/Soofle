using VkQ.Application.Abstractions.Links.ServicesInterfaces;
using VkQ.Application.Abstractions.Participants.ServicesInterfaces;
using VkQ.Application.Abstractions.Payments.ServicesInterfaces;
using VkQ.Application.Abstractions.Proxies.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.ReportProcessing;
using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.ServicesInterfaces.Manage;
using VkQ.Application.Abstractions.Vk.ServicesInterfaces;
using VkQ.Application.Services.Services.Links;
using VkQ.Application.Services.Services.Participants;
using VkQ.Application.Services.Services.Payments;
using VkQ.Application.Services.Services.Proxy;
using VkQ.Application.Services.Services.ReportsManagement;
using VkQ.Application.Services.Services.ReportsProcessors.Initializers;
using VkQ.Application.Services.Services.ReportsQuery;
using VkQ.Application.Services.Services.ReportsQuery.Mappers;
using VkQ.Application.Services.Services.Users.Manage;
using VkQ.Application.Services.Services.Vk;

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
        services.AddScoped<IReportProcessorService, IReportProcessorService>();
        services.AddScoped<IReportInitializerService, ReportInitializerService>();
        services.AddScoped<IProfileService, UserProfileService>();
        services.AddScoped<IUserParametersService, UserParametersService>();
        services.AddScoped<IVkManager, VkManager>();
    }
}