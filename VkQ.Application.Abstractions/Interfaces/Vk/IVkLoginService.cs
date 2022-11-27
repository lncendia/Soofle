using VkQ.Application.Abstractions.DTO.Users;

namespace VkQ.Application.Abstractions.Interfaces.Vk;

public interface IVkLoginService
{
    Task DeactivateAsync(VkLogoutDto info);

    Task<string> ActivateTwoFactorAsync(VkLoginDto info, string? code);
}