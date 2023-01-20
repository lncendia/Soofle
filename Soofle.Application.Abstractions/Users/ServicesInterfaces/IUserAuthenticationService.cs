using Microsoft.AspNetCore.Identity;
using Soofle.Application.Abstractions.Users.DTOs;
using Soofle.Application.Abstractions.Users.Entities;

namespace Soofle.Application.Abstractions.Users.ServicesInterfaces;

public interface IUserAuthenticationService
{
    Task CreateAsync(UserCreateDto userDto, string confirmUrl);
    Task<UserData> AuthenticateAsync(string username, string password);
    Task ResetPasswordAsync(string email, string code, string newPassword);
    Task RequestResetPasswordAsync(string email, string resetUrl);
    Task<UserData> CodeAuthenticateAsync(string userId, string code);
    Task<UserData> ExternalAuthenticateAsync(ExternalLoginInfo info);
}