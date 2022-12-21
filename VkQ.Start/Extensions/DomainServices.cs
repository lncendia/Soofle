using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Infrastructure.DataStorage;

namespace VkQ.Start.Extensions;

internal static class DomainServices
{
    internal static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}