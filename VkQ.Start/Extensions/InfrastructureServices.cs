using Hangfire;
using Hangfire.SqlServer;
using Newtonsoft.Json;
using VkQ.Application.Abstractions.Payments.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.BackgroundScheduler;
using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.ServicesInterfaces.UsersAuthentication;
using VkQ.Application.Abstractions.Vk.ServicesInterfaces;
using VkQ.Infrastructure.AntiCaptcha.AntiCaptcha;
using VkQ.Infrastructure.BackgroundTasks;
using VkQ.Infrastructure.Mailing;
using VkQ.Infrastructure.PaymentSystem.Services;
using VkQ.Infrastructure.Publications.Services;
using VkQ.Infrastructure.VkAuthentication.Services;

namespace VkQ.Start.Extensions;

internal static class InfrastructureServices
{
    internal static void AddInfrastructureServices(this IServiceCollection services, string hangfireConnectionString)
    {
        services.AddScoped<IPaymentCreatorService, PaymentService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICaptchaSolver, CaptchaSolver>();
        services.AddScoped<IJobScheduler, JobScheduler>();
        services.AddScoped<IPublicationGetterService, GetPublicationsService>();
        services.AddScoped<IPublicationInfoService, GetInfoService>();
        services.AddScoped<IParticipantsGetterService, GetParticipantsService>();
        services.AddScoped<IVkLoginService, VkLoginService>();
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

            globalConfiguration.UseSerializerSettings(new JsonSerializerSettings
                { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            // RecurringJob.AddOrUpdate<ISubscribeDeleter>("subscribesChecker", x => x.DeleteAsync(), Cron.Daily);
            // RecurringJob.AddOrUpdate<IWorkDeleter>("worksChecker", x => x.DeleteAsync(), Cron.Daily);
        });
        services.AddHangfireServer(options => options.WorkerCount = Environment.ProcessorCount * 15);
    }
}