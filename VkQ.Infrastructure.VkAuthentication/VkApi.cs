using System.Net;
using LikeBotVK.Infrastructure.VkAuthentication.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;
using VkNet.Utils.AntiCaptcha;
using VkQ.Application.Abstractions.DTO.Proxy;
using VkQ.Application.Abstractions.DTO.Users;
using VkQ.Infrastructure.VkAuthentication.AntiCaptcha;

namespace VkQ.Infrastructure.VkAuthentication;

public static class VkApi
{
    private static VkNet.VkApi BuildLoginApi(VkLoginDto info, VkQ.Domain.Abstractions.Services.ICaptchaSolver solver)
    {
        var services = new ServiceCollection();
        services.AddAudioBypass();
        services.AddScoped<ICaptchaSolver, CaptchaSolver>(_ => new CaptchaSolver(solver));
        services.AddSingleton(_ => GetHttpClientWithProxy(info.Proxy));
        var api = new VkNet.VkApi(services);
        return api;
    }

    private static async Task<VkNet.VkApi> BuildTokenApiAsync(VkLogoutDto info, VkQ.Domain.Abstractions.Services.ICaptchaSolver solver)
    {
        var services = new ServiceCollection();
        services.AddScoped<ICaptchaSolver, CaptchaSolver>(_ => new CaptchaSolver(solver));
        services.AddSingleton(_ => GetHttpClientWithProxy(info.Proxy));

        var api = new VkNet.VkApi(services);
        await api.AuthorizeAsync(new ApiAuthParams { AccessToken = info.Token });
        return api;
    }

    private static HttpClient GetHttpClientWithProxy(ProxyDto proxy)
    {
        var httpClientHandler = new HttpClientHandler
        {
            Proxy = new WebProxy(proxy.ProxyHost, proxy.ProxyPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(proxy.ProxyLogin, proxy.ProxyPassword)
            }
        };
        return new HttpClient(httpClientHandler);
    }

    public static async Task<string> ActivateAsync(VkLoginDto info,
        VkQ.Domain.Abstractions.Services.ICaptchaSolver solver)
    {
        var api = BuildLoginApi(info, solver);

        await api.AuthorizeAsync(new ApiAuthParams
        {
            Login = info.Login,
            Password = info.Password,
            TwoFactorAuthorization = () => throw new TwoFactorRequiredException()
        });
        return api.Token;
    }

    public static async Task DeactivateAsync(VkLogoutDto info, VkQ.Domain.Abstractions.Services.ICaptchaSolver solver)
    {
        var api = await BuildTokenApiAsync(info, solver);
        await api.LogOutAsync();
    }

    public static async Task<string> ActivateWithTwoFactorAsync(VkLoginDto info, string code,
        VkQ.Domain.Abstractions.Services.ICaptchaSolver solver)
    {
        var api = BuildLoginApi(info, solver);

        await api.AuthorizeAsync(new ApiAuthParams
        {
            Login = info.Login,
            Password = info.Password,
            TwoFactorAuthorization = () => code
        });
        return api.Token;
    }
}