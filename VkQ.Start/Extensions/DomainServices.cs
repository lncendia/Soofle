using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Infrastructure.DataStorage;
using VkQ.Infrastructure.DataStorage.Context;

namespace VkQ.Start.Extensions;

internal static class DomainServices
{
    internal static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services
            .AddScoped<IPublicationReportBuilder<LikeReport>,
                VkQ.Domain.Services.Services.Builders.LikeReportBuilder>();
    }
}