using Microsoft.EntityFrameworkCore;
using Soofle.Application.Abstractions.Jobs.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Infrastructure.ApplicationDataStorage;
using Soofle.Infrastructure.DataStorage;
using Soofle.Infrastructure.DataStorage.Context;
using Soofle.Start.Exceptions;

namespace Soofle.Start.Extensions;

internal static class PersistenceServices
{
    internal static void AddPersistenceServices(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        var connectionString = configuration!.GetConnectionString("Main") ??
                               throw new ConfigurationException("ConnectionStrings:Main");
        var applicationConnectionString = configuration!.GetConnectionString("Application") ??
                                          throw new ConfigurationException("ConnectionStrings:Application");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(applicationConnectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJobStorage, JobStorage>();
    }
}