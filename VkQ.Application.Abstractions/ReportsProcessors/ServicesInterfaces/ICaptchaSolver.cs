namespace VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface ICaptchaSolver
{
    Task<(long id, string response)> SolveAsync(string url);
    Task CaptchaIsFalseAsync(long id);
}