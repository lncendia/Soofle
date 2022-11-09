using Microsoft.AspNetCore.Identity;
using VkQ.Application.Abstractions.Entities;
using VkQ.Application.Abstractions.Exceptions.UsersAuthentication;
using VkQ.Application.Abstractions.Interfaces.UsersAuthentication;
using VkQ.Application.Services.Services.Authentication.Entities;
using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Users.Specification;

namespace VkQ.Application.Services.Services.Authentication;

public class UserSecurityService : IUserSecurityService
{
    private readonly UserManager<UserData> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public UserSecurityService(UserManager<UserData> userManager, IEmailService emailService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task RequestResetEmailAsync(string email, string newEmail, string resetUrl)
    {
        if (email == newEmail)
            throw new ArgumentException("The new email should be different from the current one.", nameof(newEmail));
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null || !await _userManager.IsEmailConfirmedAsync(user)) throw new UserNotFoundException();
        var code = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        var url = resetUrl + $"?email={Uri.EscapeDataString(email)}&code={Uri.EscapeDataString(code)}";
        try
        {
            await _emailService.SendEmailAsync(email,
                $"Подтвердите смену почты, перейдя по <a href = \"{url}\">ссылке</a>.");
        }
        catch (Exception ex)
        {
            throw new EmailException(ex);
        }
    }

    public async Task ResetEmailAsync(string email, string newEmail, string code)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) throw new UserNotFoundException();
        var result = await _userManager.ChangeEmailAsync(user, newEmail, code);
        if (!result.Succeeded) throw new InvalidCodeException();
        var userDomain =
            (await _unitOfWork.UserRepository.Value.FindAsync(new UserByEmailSpecification(email), null, 0, 1))
            .FirstOrDefault();
        if (userDomain == null) throw new UserNotFoundException();
        userDomain.Email = newEmail;
        await _unitOfWork.UserRepository.Value.UpdateAsync(userDomain);
        await _unitOfWork.SaveAsync();
    }

    public async Task ChangePasswordAsync(string email, string oldPassword, string newPassword)
    {
        if (oldPassword == newPassword)
        {
            throw new ArgumentException("The new password should be different from the current one.",
                nameof(newPassword));
        }
        //todo: exceptions
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) throw new UserNotFoundException();
        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        if (!result.Succeeded)
            throw new ArgumentException("The old password is specified incorrectly.", nameof(oldPassword));
    }
}