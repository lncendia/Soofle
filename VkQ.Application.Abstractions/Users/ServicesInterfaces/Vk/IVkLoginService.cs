using VkQ.Application.Abstractions.Users.DTOs;

namespace VkQ.Application.Abstractions.Users.ServicesInterfaces.Vk;

public interface IVkLoginService
{
    Task DeactivateAsync(VkLogoutDto info);

    Task<string> ActivateTwoFactorAsync(VkLoginDto info, string? code);
}