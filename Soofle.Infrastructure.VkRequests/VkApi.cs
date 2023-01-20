using System.Net;
using Microsoft.Extensions.DependencyInjection;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Utils.AntiCaptcha;
using Soofle.Application.Abstractions.VkRequests.DTOs;
using Soofle.Application.Abstractions.VkRequests.Exceptions;
using Soofle.Infrastructure.VkRequests.AntiCaptcha;

namespace Soofle.Infrastructure.VkRequests;

internal static class VkApi
{
    public static async Task<VkNet.VkApi> BuildApiAsync(RequestInfo info,
        Soofle.Application.Abstractions.VkRequests.ServicesInterfaces.ICaptchaSolver captchaSolver)
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

    public static Exception GetException(VkApiException ex)
    {
        var code = ex switch
        {
            UserAccessDeniedException exception => exception.ErrorCode,
            UserAuthorizationFailException exception => exception.ErrorCode,
            UserDeletedOrBannedException exception => exception.ErrorCode,
            CaptchaNeededException exception => exception.ErrorCode,
            CommentsPostAccessDeniedException exception => exception.ErrorCode,
            _ => ex.ErrorCode
        };
        return new VkRequestException(code, ex);
    }
}