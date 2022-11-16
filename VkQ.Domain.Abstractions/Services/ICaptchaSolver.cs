namespace VkQ.Domain.Abstractions.Services;

public interface ICaptchaSolver
{
    Task<(long id, string response)> SolveAsync(string url);
    Task CaptchaIsFalseAsync(long id);
}