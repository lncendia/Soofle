using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Soofle.Application.Abstractions.Profile.ServicesInterfaces;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.WEB.ViewModels.Settings;
using Soofle.Domain.Users.Exceptions;
using ChangePasswordViewModel = Soofle.WEB.ViewModels.Settings.ChangePasswordViewModel;

namespace Soofle.WEB.Controllers;

[Authorize]
public class SettingsController : Controller
{
    private readonly IUserParametersService _userService;

    public SettingsController(IUserParametersService userService)
    {
        _userService = userService;
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _userService.RequestResetEmailAsync(userId, model.Email, Url.Action(
                "AcceptChangeEmail", "Settings", null, HttpContext.Request.Scheme)!);

            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                EmailException => "Произошла ошибка при отправке письма",
                UserAlreadyExistException => "Пользователь с такой почтой уже зарегистрирован",
                ArgumentException => "Некорректные данные",
                _ => "Произошла ошибка при изменении почты"
            };

            return BadRequest(text);
        }
    }

    [HttpGet]
    public async Task<IActionResult> AcceptChangeEmail(AcceptChangeEmailViewModel model)
    {
        if (!ModelState.IsValid) return RedirectToAction("ChangeEmail", new { message = "Ссылка недействительна" });

        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _userService.ResetEmailAsync(userId, model.Email, model.Code);
            await ChangeClaimAsync(ClaimTypes.Email, model.Email);
            return RedirectToAction("Index", "Home", new { message = "Почта успешно изменена" });
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь с таким адресом не найден",
                InvalidCodeException => "Ссылка недействительна",
                UserAlreadyExistException => "Пользователь с такой почтой уже зарегистрирован",
                InvalidEmailException => "Неверный формат почты",
                _ => "Произошла ошибка при смене почты"
            };

            return RedirectToAction("Index", "Home", new { message = text });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            await _userService.ChangePasswordAsync(email, model.OldPassword,
                model.Password);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                ArgumentException => "Некорректные данные",
                _ => "Произошла ошибка при изменении пароля"
            };

            return BadRequest(text);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeName(ChangeNameViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _userService.ChangeNameAsync(userId, model.Name);
            await ChangeClaimAsync(ClaimTypes.Name, model.Name);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                InvalidNicknameException => "Некорректные данные",
                _ => "Произошла ошибка при изменении имени"
            };

            return BadRequest(text);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeTarget(ChangeTargetViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _userService.ChangeTargetAsync(userId, model.Target!.Value);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                InvalidNicknameException => "Некорректные данные",
                _ => "Произошла ошибка при изменении имени"
            };

            return BadRequest(text);
        }
    }


    private async Task ChangeClaimAsync(string key, string value)
    {
        if (HttpContext.User.Identity is ClaimsIdentity claimsIdentity)
        {
            var claim = claimsIdentity.FindFirst(key);
            if (claimsIdentity.TryRemoveClaim(claim))
            {
                claimsIdentity.AddClaim(new Claim(key, value));
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);
            }
        }
    }
}