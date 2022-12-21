using VkQ.WEB.Mappers.Abstractions;
using VkQ.WEB.Mappers.ElementMapper;
using VkQ.WEB.Mappers.ReportMapper;

namespace VkQ.Start.Extensions;

internal static class WebServices
{
    internal static void AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<IReportMapper, ReportMapper>();
        services.AddScoped<IElementMapper, ElementMapper>();
    }
}