namespace VkQ.Infrastructure.AntiCaptcha.Interfaces;

public interface ICaptchaSolver
{
    Task<(long id, string response)> SolveAsync(string url);
    Task CaptchaIsFalseAsync(long id);
}