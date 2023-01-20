namespace Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;

public interface ICaptchaSolver
{
    Task<(long id, string response)> SolveAsync(string url);
    Task CaptchaIsFalseAsync(long id);
}