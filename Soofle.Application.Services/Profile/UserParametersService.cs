﻿using Microsoft.AspNetCore.Identity;
using Soofle.Application.Abstractions.Profile.ServicesInterfaces;
using Soofle.Application.Abstractions.Users.Entities;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.Application.Abstractions.Users.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Users.Exceptions;

namespace Soofle.Application.Services.Profile;

public class UserParametersService : IUserParametersService
{
    private readonly UserManager<UserData> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public UserParametersService(UserManager<UserData> userManager, IEmailService emailService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task RequestResetEmailAsync(Guid userId, string newEmail, string resetUrl)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        if (user.Email == newEmail)
            throw new ArgumentException("The new email should be different from the current one.", nameof(newEmail));
        var userApplication = await _userManager.FindByEmailAsync(user.Email);
        if (userApplication is null || !userApplication.EmailConfirmed) throw new UserNotFoundException();
        var code = await _userManager.GenerateChangeEmailTokenAsync(userApplication, newEmail);
        var url = resetUrl + $"?email={Uri.EscapeDataString(newEmail)}&code={Uri.EscapeDataString(code)}";
        try
        {
            await _emailService.SendEmailAsync(user.Email,
                $"Подтвердите смену почты, перейдя по <a href = \"{url}\">ссылке</a>.");
        }
        catch (Exception ex)
        {
            throw new EmailException(ex);
        }
    }

    public async Task ResetEmailAsync(Guid userId, string newEmail, string code)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        var userApplication = await _userManager.FindByEmailAsync(user.Email);
        if (userApplication is null) throw new UserNotFoundException();
        var result = await _userManager.ChangeEmailAsync(userApplication, newEmail, code);
        if (!result.Succeeded)
        {
            var ex = result.Errors.First().Code switch
            {
                "MailUsed" => new UserAlreadyExistException(),
                "MailIncorrect" => new InvalidEmailException(newEmail),
                "InvalidToken" => new InvalidCodeException(),
                _ => new Exception(result.Errors.First().Description)
            };
            throw ex;
        }

        user.Email = newEmail;
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task ChangePasswordAsync(string email, string? oldPassword, string newPassword)
    {
        if (oldPassword == newPassword)
        {
            throw new ArgumentException("The new password should be different from the current one.",
                nameof(newPassword));
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) throw new UserNotFoundException();
        IdentityResult result;
        if (user.PasswordHash == null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
        else
        {
            result = await _userManager.ChangePasswordAsync(user, oldPassword!, newPassword);
        }

        if (!result.Succeeded)
            throw new ArgumentException("The old password is specified incorrectly.", nameof(oldPassword));
    }

    public async Task ChangeNameAsync(Guid userId, string newName)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        user.Name = newName;
        var userApplication = await _userManager.FindByEmailAsync(user.Email);
        if (userApplication is null) throw new UserNotFoundException();
        await _userManager.SetUserNameAsync(userApplication, newName);
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task ChangeTargetAsync(Guid userId, long target)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        user.ChatId = target;
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}