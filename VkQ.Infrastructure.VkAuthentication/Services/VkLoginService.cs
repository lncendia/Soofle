using VkNet.AudioBypassService.Exceptions;
using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using VkQ.Application.Abstractions.Vk.DTOs;
using VkQ.Application.Abstractions.Vk.Exceptions;
using VkQ.Application.Abstractions.Vk.ServicesInterfaces;

namespace VkQ.Infrastructure.VkAuthentication.Services;

public class VkLoginService : IVkLoginService
{
    private readonly ICaptchaSolver _solver;

    public VkLoginService(ICaptchaSolver solver) => _solver = solver;

    public async Task<string> ActivateAsync(VkLoginDto info)
    {
        try
        {
            return await VkApi.ActivateAsync(info, _solver);
        }
        catch (VkAuthException)
        {
            throw new InvalidCredentialsException();
        }
        catch (Exception ex) when (ex is not TwoFactorRequiredException)
        {
            throw new ErrorActiveVkException(ex.Message, ex);
        }
    }

    public async Task<string> ActivateTwoFactorAsync(VkLoginDto info, string code)
    {
        try
        {
            return await VkApi.ActivateWithTwoFactorAsync(info, code, _solver);
        }
        catch (VkAuthException)
        {
            throw new InvalidCredentialsException();
        }
        catch (Exception ex)
        {
            throw new ErrorActiveVkException(ex.Message, ex);
        }
    }

    public async Task DeactivateAsync(VkLogoutDto info)
    {
        try
        {
            await VkApi.DeactivateAsync(info, _solver);
        }
        catch (Exception ex)
        {
            throw new ErrorActiveVkException(ex.Message, ex);
        }
    }
}