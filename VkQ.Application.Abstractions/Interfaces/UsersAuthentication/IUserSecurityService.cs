﻿namespace VkQ.Application.Abstractions.Interfaces.UsersAuthentication;

public interface IUserSecurityService
{
    Task RequestResetEmailAsync(string email, string newEmail, string resetUrl);
    Task ResetEmailAsync(string email, string newEmail, string code);
    Task ChangePasswordAsync(string email, string oldPassword, string newPassword);
}