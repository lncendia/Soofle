using Microsoft.AspNetCore.Identity;
using VkQ.Application.Abstractions.Users.DTOs;
using VkQ.Application.Abstractions.Users.Entities;

namespace VkQ.Application.Abstractions.Users.ServicesInterfaces.UsersAuthentication;

public interface IUserAuthenticationService
{
    Task CreateAsync(UserDto userDto, string confirmUrl);
    Task<UserData> AuthenticateAsync(string username, string password);
    Task ResetPasswordAsync(string email, string code, string newPassword);
    Task RequestResetPasswordAsync(string email, string resetUrl);
    Task<UserData> AcceptCodeAsync(string userId, string code);
    Task<UserData> ExternalLoginAsync(ExternalLoginInfo info);
}