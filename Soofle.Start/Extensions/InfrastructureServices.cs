using Soofle.Application.Abstractions.Payments.ServicesInterfaces;
using Soofle.Application.Abstractions.Users.ServicesInterfaces;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;
using Soofle.Infrastructure.Mailing;
using Soofle.Infrastructure.PaymentSystem.Services;
using Soofle.Infrastructure.VkRequests.AntiCaptcha;
using Soofle.Infrastructure.VkRequests.Services;
using Soofle.Start.Exceptions;

namespace Soofle.Start.Extensions;

internal static class InfrastructureServices
{
    internal static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // var qiwiToken = configuration!.GetValue<string>("Payments:QiwiToken") ??
        //                 throw new ConfigurationException("Payments:QiwiToken");
        //services.AddScoped<IPaymentCreatorService, PaymentService>(_=> new PaymentService(qiwiToken));
        var login = configuration.GetValue<string>("SMTP:Login") ?? throw new ConfigurationException("SMTP:Login");
        var password = configuration.GetValue<string>("SMTP:Password") ??
                       throw new ConfigurationException("SMTP:Password");
        var host = configuration.GetValue<string>("SMTP:Host") ?? throw new ConfigurationException("SMTP:Host");
        var port = configuration.GetValue<int?>("SMTP:Port") ?? throw new ConfigurationException("SMTP:Port");
        var token = configuration.GetValue<string>("AntiCaptchaToken") ??
                    throw new ConfigurationException("AntiCaptchaToken");


        services.AddScoped<IPaymentCreatorService, TestPaymentService>();
        services.AddScoped<IEmailService, EmailService>(_ => new EmailService(login, password, host, port));
        services.AddScoped<ICaptchaSolver, CaptchaSolver>(_ => new CaptchaSolver(token));
        services.AddScoped<IPublicationService, GetPublicationsService>();
        services.AddScoped<ILikeInfoService, LikeInfoService>();
        services.AddScoped<ICommentInfoService, CommentInfoService>();
        services.AddScoped<IParticipantsService, GetParticipantsService>();
    }
}