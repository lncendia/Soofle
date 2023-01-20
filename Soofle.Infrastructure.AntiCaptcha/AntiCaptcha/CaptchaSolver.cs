using CaptchaSharp;
using CaptchaSharp.Enums;
using CaptchaSharp.Models;
using CaptchaSharp.Services;
using Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;

namespace Soofle.Infrastructure.AntiCaptcha.AntiCaptcha;

public class CaptchaSolver : ICaptchaSolver
{
    private readonly CaptchaService _service;

    public CaptchaSolver(string token)
    {
        _service = new AntiCaptchaService(token);
    }

    public async Task<(long id, string response)> SolveAsync(string url)
    {
        using var client = new HttpClient();
        var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
        using var ms = new MemoryStream();
        await (await response.Content.ReadAsStreamAsync()).CopyToAsync(ms);
        var data = Convert.ToBase64String(ms.GetBuffer());
        var captcha = await _service.SolveImageCaptchaAsync(data, new ImageCaptchaOptions
        {
            CaptchaLanguage = CaptchaLanguage.English, MinLength = 4
        });
        return (captcha.Id, captcha.Response);
    }

    public Task CaptchaIsFalseAsync(long id) => _service.ReportSolution(id, CaptchaType.ImageCaptcha);
}