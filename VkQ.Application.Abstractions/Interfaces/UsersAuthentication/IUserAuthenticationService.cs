using Microsoft.AspNetCore.Identity;
using VkQ.Application.Abstractions.DTO.Users;

namespace VkQ.Application.Abstractions.Interfaces.UsersAuthentication;

public interface IUserAuthenticationService
{
    Task CreateAsync(UserDto userDto, string confirmUrl);
    Task AuthenticateAsync(string username, string password);
    Task ResetPasswordAsync(string email, string code, string newPassword);
    Task RequestResetPasswordAsync(string email, string resetUrl);
    Task AcceptCodeAsync(string userId, string code);
    Task ExternalLoginAsync(ExternalLoginInfo info);
}