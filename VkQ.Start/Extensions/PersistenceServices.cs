using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Infrastructure.DataStorage;
using VkQ.Infrastructure.DataStorage.Context;

namespace VkQ.Start.Extensions;

internal static class PersistenceServices
{
    internal static void AddPersistenceServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}