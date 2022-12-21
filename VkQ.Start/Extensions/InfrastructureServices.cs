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
    internal static void AddInfrastructureServices(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        var hangfireConnectionString = configuration!.GetConnectionString("Hangfire") ?? throw new Exception();
        var captchaToken = configuration!.GetValue<string>("AntiCaptchaToken") ?? throw new Exception();
        var qiwiToken = configuration!.GetSection("Payments").GetValue<string>("QiwiToken") ?? throw new Exception();
        var smtpSection = configuration!.GetSection("SMTP");
        var smtpData = new
        {
            Login = smtpSection["Login"] ?? throw new Exception(),
            Password = smtpSection["Password"] ?? throw new Exception(),
            Host = smtpSection["Host"] ?? throw new Exception()
        };
        services.AddScoped<IPaymentCreatorService, PaymentService>(_=> new PaymentService(qiwiToken));
        services.AddScoped<IEmailService, EmailService>(_ =>
            new EmailService(smtpData.Login, smtpData.Password, smtpData.Host, 80));
        services.AddScoped<ICaptchaSolver, CaptchaSolver>(_=>new CaptchaSolver(captchaToken));
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
            // RecurringJob.AddOrUpdate<IWorkDeleter>("worksChecker", x => x.DeleteAsync(), Cron.Daily);
        });
        services.AddHangfireServer(options => options.WorkerCount = Environment.ProcessorCount * 15);
    }
}