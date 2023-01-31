using Hangfire;
using Hangfire.SqlServer;
using Soofle.Application.Abstractions.BackgroundJobs.ServicesInterfaces;
using Soofle.Application.Abstractions.Jobs.ServicesInterfaces;
using Soofle.Application.Services.BackgroundJobs;
using Soofle.Infrastructure.BackgroundTasks;
using Soofle.Start.Exceptions;

namespace Soofle.Start.Extensions;

internal static class HangfireServices
{
    internal static void AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
    {
        var hangfireConnectionString = configuration.GetConnectionString("Hangfire") ??
                                       throw new ConfigurationException("ConnectionStrings:Hangfire");
        services.AddScoped<IJobScheduler, JobScheduler>();
        services.AddScoped<IReportsRemovingService, ReportsRemovingService>();
        services.AddHangfire((_, globalConfiguration) =>
        {
            globalConfiguration.UseSqlServerStorage(hangfireConnectionString,
                new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true,
                    PrepareSchemaIfNecessary = true
                });
            RecurringJob.AddOrUpdate<IReportsRemovingService>("reportsChecker", x => x.CheckAndRemoveAsync(),
                Cron.Daily);
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3, DelaysInSeconds = new[] { 300 } });
        });
        services.AddHangfireServer(options => options.WorkerCount = Environment.ProcessorCount * 5);
    }
}