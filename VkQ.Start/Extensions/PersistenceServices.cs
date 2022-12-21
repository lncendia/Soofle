using Microsoft.EntityFrameworkCore;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.BackgroundScheduler;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Infrastructure.ApplicationDataStorage;
using VkQ.Infrastructure.DataStorage;
using VkQ.Infrastructure.DataStorage.Context;

namespace VkQ.Start.Extensions;

internal static class PersistenceServices
{
    internal static void AddPersistenceServices(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        var connectionString = configuration!.GetConnectionString("Main") ?? throw new Exception();
        var applicationConnectionString = configuration!.GetConnectionString("Application") ?? throw new Exception();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(applicationConnectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJobStorage, JobStorage>();
    }
}