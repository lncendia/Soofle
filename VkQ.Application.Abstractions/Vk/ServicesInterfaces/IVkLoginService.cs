using VkQ.Application.Abstractions.Vk.DTOs;

namespace VkQ.Application.Abstractions.Vk.ServicesInterfaces;

public interface IVkLoginService
{
    Task DeactivateAsync(VkLogoutDto info);

    Task<string> ActivateAsync(VkLoginDto info);
    Task<string> ActivateTwoFactorAsync(VkLoginDto info, string code);
}