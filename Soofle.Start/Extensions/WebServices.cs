using Soofle.WEB.Mappers.Abstractions;
using Soofle.WEB.Mappers.ElementMapper;
using Soofle.WEB.Mappers.ReportMapper;

namespace Soofle.Start.Extensions;

internal static class WebServices
{
    internal static void AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<IReportMapper, ReportMapper>();
        services.AddScoped<IElementMapper, ElementMapper>();
    }
}