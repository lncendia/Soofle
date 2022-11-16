using VkQ.Application.Abstractions.DTO.Users;

namespace VkQ.Application.Abstractions.Interfaces.VkAuthentication;

public interface IVkLoginService
{
    Task<string> ActivateAsync(VkLoginDto info);

    Task DeactivateAsync(VkLogoutDto info);

    Task<string> ActivateTwoFactorAsync(VkLoginDto info, string code);
}