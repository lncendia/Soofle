using System.Net;
using Microsoft.Extensions.DependencyInjection;
using VkNet.Model;
using VkNet.Utils.AntiCaptcha;
using VkQ.Infrastructure.Publications.AntiCaptcha;

namespace VkQ.Infrastructure.Publications;

public static class VkApi
{
    public static VkNet.VkApi BuildApi(string accessToken, ProxyDto? proxy,
        VkQ.Infrastructure.AntiCaptcha.Interfaces.ICaptchaSolver captchaSolver)
    {
        var services = new ServiceCollection();
        services.AddScoped<ICaptchaSolver, CaptchaSolver>(_ => new CaptchaSolver(antiCaptcha));
        services.AddAudioBypass();
        if (proxy != null)
            services.AddSingleton(_ => GetHttpClientWithProxy(proxy));

        var api = new VkNet.VkApi(services);
        api.Authorize(new ApiAuthParams {AccessToken = accessToken});
        return api;
    }

    private static HttpClient GetHttpClientWithProxy(Proxy proxy)
    {
        var httpClientHandler = new HttpClientHandler
        {
            Proxy = new WebProxy(proxy.Host, proxy.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(proxy.Login, proxy.Password)
            }
        };
        return new HttpClient(httpClientHandler);
    }
}