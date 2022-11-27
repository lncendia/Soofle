using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Infrastructure.DataStorage;
using VkQ.Infrastructure.DataStorage.Context;

namespace VkQ.Start.Extensions;

internal static class DomainServices
{
    internal static void AddDomainServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}