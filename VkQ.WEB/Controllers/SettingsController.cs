using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Application.Abstractions.Users.ServicesInterfaces.Manage;
using VkQ.Domain.Users.Exceptions;
using VkQ.WEB.ViewModels.Settings;
using VkQ.WEB.ViewModels.Users;
using ChangePasswordViewModel = VkQ.WEB.ViewModels.Settings.ChangePasswordViewModel;

namespace VkQ.WEB.Controllers;

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
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            await _userService.RequestResetEmailAsync(email, model.Email, Url.Action(
                "AcceptChangeEmail", "Settings", null, HttpContext.Request.Scheme)!);

            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                EmailException => "Произошла ошибка при отправке письма",
                ArgumentException => "Некорректные данные",
                _ => "Произошла ошибка при изменении почты"
            };

            return BadRequest(text);
        }
    }

    [HttpGet]
    public async Task<IActionResult> AcceptChangeEmail(AcceptChangeEmailViewModel model)
    {
        if (!ModelState.IsValid) return RedirectToAction("ChangeEmail", new { message = "Ссылка недействительна." });

        try
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            await _userService.ResetEmailAsync(email, model.Email, model.Code);
            return RedirectToAction("ChangeEmail", new { message = "Почта успешно изменена." });
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь с таким email не найден",
                InvalidCodeException => "Ссылка недействительна",
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
            return RedirectToAction("ChangePassword", new { message = "Пароль успешно изменен." });
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
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            await _userService.ChangeNameAsync(email, model.Name);
            return RedirectToAction("ChangeName", new { message = "Имя успешно изменено." });
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
}