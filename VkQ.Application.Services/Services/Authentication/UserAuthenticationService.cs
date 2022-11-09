using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using VkQ.Application.Abstractions.DTO.Users;
using VkQ.Application.Abstractions.Exceptions.UsersAuthentication;
using VkQ.Application.Abstractions.Interfaces.UsersAuthentication;
using VkQ.Application.Services.Services.Authentication.Entities;
using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Users.Entities;

namespace VkQ.Application.Services.Services.Authentication;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly UserManager<UserData> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public UserAuthenticationService(UserManager<UserData> userManager, IEmailService emailService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAsync(UserDto userDto, string confirmUrl)
    {
        var user = await _userManager.FindByEmailAsync(userDto.Email);
        if (user != null) throw new UserAlreadyExistException();
        var userDomain = new User(userDto.Username, userDto.Email);
        user = new UserData(userDto.Email);
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (result.Errors.Any()) throw new UserCreationException(result.Errors.First().Description);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var url = confirmUrl + $"?email={Uri.EscapeDataString(user.Email)}&code={Uri.EscapeDataString(code)}";
        try
        {
            await _emailService.SendEmailAsync(user.Email,
                $"Подтвердите регистрацию, перейдя по <a href=\"{url}\">ссылке</a>.");
            await AddAsync(userDomain);
        }
        catch (Exception ex)
        {
            await _userManager.DeleteAsync(user);
            throw new EmailException(ex);
        }
    }

    public async Task ExternalLoginAsync(ExternalLoginInfo info)
    {
        var user = await _userManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email));
        if (user != null)
        {
            var logins = await _userManager.GetLoginsAsync(user);
            if (!logins.Any(x => x.LoginProvider == info.LoginProvider && x.ProviderKey == info.ProviderKey))
                throw new UserAlreadyExistException();
        }
        else
        {
            user = new UserData(info.Principal.FindFirstValue(ClaimTypes.Email));
            var userDomain =
                new User(
                    info.Principal.FindFirstValue(ClaimTypes.GivenName) + ' ' +
                    info.Principal.FindFirstValue(ClaimTypes.Surname), user.Email);

            var result = await _userManager.CreateAsync(user);
            if (result.Errors.Any()) throw new UserCreationException(result.Errors.First().Description);
            user.EmailConfirmed = true;
            await _userManager.AddLoginAsync(user, info);
            await _userManager.UpdateAsync(user);

            await AddAsync(userDomain);
        }

        return user;
    }

    public async Task AcceptCodeAsync(string email, string code)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) throw new UserNotFoundException();
        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (!result.Succeeded) throw new InvalidCodeException();
        return user;
    }

    public async Task RequestResetPasswordAsync(string email, string resetUrl)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null || !await _userManager.IsEmailConfirmedAsync(user)) throw new UserNotFoundException();
        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        var url = resetUrl + $"?email={Uri.EscapeDataString(email)}&code={Uri.EscapeDataString(code)}";
        try
        {
            await _emailService.SendEmailAsync(email,
                $"Подтвердите смену пароля, перейдя по <a href = \"{url}\">ссылке</a>.");
        }
        catch (Exception ex)
        {
            await _userManager.DeleteAsync(user);
            throw new EmailException(ex);
        }
    }

    public async Task ResetPasswordAsync(string email, string code, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) throw new UserNotFoundException();
        var result = await _userManager.ResetPasswordAsync(user, code, newPassword);
        if (!result.Succeeded) throw new InvalidCodeException();
    }

    public async Task AuthenticateAsync(string username, string password)
    {
        var user = await _userManager.FindByEmailAsync(username);
        if (user == null) throw new UserNotFoundException();
        var success = await _userManager.CheckPasswordAsync(user, password);
        if (!success) throw new InvalidPasswordException();
        return user;
    }

    private async Task AddAsync(User user)
    {
        await _unitOfWork.UserRepository.Value.AddAsync(user);
        await _unitOfWork.SaveAsync();
    }
}