using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Infrastructure.DataStorage;

namespace Soofle.Start.Extensions;

internal static class DomainServices
{
    internal static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}