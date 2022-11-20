using System.Net;
using Microsoft.Extensions.DependencyInjection;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Utils.AntiCaptcha;
using VkQ.Domain.Abstractions.DTOs;
using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Infrastructure.Publications.AntiCaptcha;

namespace VkQ.Infrastructure.Publications;

public static class VkApi
{
    public static async Task<VkNet.VkApi> BuildApiAsync(RequestInfo info,
        Domain.Abstractions.Services.ICaptchaSolver captchaSolver)
    {
        var services = new ServiceCollection();
        services.AddScoped<ICaptchaSolver, CaptchaSolver>(_ => new CaptchaSolver(captchaSolver));
        services.AddSingleton(_ => GetHttpClientWithProxy(info));

        var api = new VkNet.VkApi(services);
        await api.AuthorizeAsync(new ApiAuthParams { AccessToken = info.Token });
        return api;
    }

    private static HttpClient GetHttpClientWithProxy(RequestInfo info)
    {
        var httpClientHandler = new HttpClientHandler
        {
            Proxy = new WebProxy(info.Host, info.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(info.Login, info.Password)
            }
        };
        return new HttpClient(httpClientHandler);
    }

    private static void ThrowException(VkApiMethodInvokeException ex) =>
        throw new VkRequestException(ex.ErrorCode, ex.Message, ex);
}