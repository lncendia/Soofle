using VkQ.Application.Abstractions.Users.Entities;

namespace VkQ.Application.Abstractions.Users.ServicesInterfaces.Manage;

public interface IUserParametersService
{
    Task RequestResetEmailAsync(string email, string newEmail, string resetUrl);
    Task<UserData> ResetEmailAsync(string email, string newEmail, string code);
    Task ChangePasswordAsync(string email, string oldPassword, string newPassword);
    Task<UserData> ChangeNameAsync(string email, string newName);
}