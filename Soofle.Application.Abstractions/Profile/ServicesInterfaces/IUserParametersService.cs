namespace Soofle.Application.Abstractions.Profile.ServicesInterfaces;

public interface IUserParametersService
{
    Task RequestResetEmailAsync(Guid userId, string newEmail, string resetUrl);
    Task ResetEmailAsync(Guid userId, string newEmail, string code);
    Task ChangePasswordAsync(string email, string? oldPassword, string newPassword);
    Task ChangeNameAsync(Guid userId, string newName);
    Task ChangeTargetAsync(Guid userId, long target);
}