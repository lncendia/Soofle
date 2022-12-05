using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Links.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Application.Abstractions.Users.ServicesInterfaces.UsersAuthentication;
using VkQ.Domain.Users.Exceptions;
using VkQ.WEB.ViewModels.Settings;

namespace VkQ.WEB.Controllers;

[Authorize]
public class SettingsController : Controller
{
    private readonly IUserSecurityService _userService;
    private readonly ILinkManager _linkManager;

    public SettingsController(IUserSecurityService userManager, ILinkManager linkManager)
    {
        _userService = userManager;
        _linkManager = linkManager;
    }

    [HttpGet]
    public async Task<IActionResult> Communications(string message)
    {
        if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        var links = await _linkManager.GetLinksAsync(id);

        var data = new CommunicationsViewModel
        {
            Links = _communicationLink.GetCommunications(user),
            CurrentUser = user,
            Scheme = HttpContext.Request.Scheme
        };
        return View(data);
    }


    [HttpGet]
    public IActionResult CreateCommunication()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCommunication(Guid userId)
    {
        if (!ModelState.IsValid) return View(userId);
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        await _linkManager.AddLinkAsync(id, userId);
        return RedirectToAction("Communications", new { message = "Приглашение успешно создано." });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCommunication(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        await _linkManager.RemoveLinkAsync(id, userId);
        return Ok();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AcceptCommunication(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        await _linkManager.AcceptLinkAsync(id, userId);
        return Ok();
    }


    [HttpGet]
    public IActionResult ChangeEmail(string message)
    {
        if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
        return View(new ChangeEmailViewModel { Email = User.Identity!.Name! });
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try
        {
            await _userService.RequestResetEmailAsync(User.Identity!.Name!, model.Email, Url.Action(
                "AcceptChangeEmail",
                "Settings", null,
                HttpContext.Request.Scheme)!);

            return RedirectToAction("ChangeEmail",
                new
                {
                    message = "Для завершения проверьте электронную почту и перейдите по ссылке, указанной в письме."
                });
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

            ModelState.AddModelError("", text);
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> AcceptChangeEmail(string? email, string? code)
    {
        if (code == null || email == null)
            return RedirectToAction("ChangeEmail", new { message = "Ссылка недействительна." });

        try
        {
            await _userService.ResetEmailAsync(User.Identity!.Name!, email, code);
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

            return RedirectToAction("ChangeEmail", new { message = text });
        }
    }

    [HttpGet]
    public IActionResult ChangePassword(string message)
    {
        if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try
        {
            await _userService.ChangePasswordAsync(User.Identity!.Name!, model.OldPassword,
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

            ModelState.AddModelError("", text);
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult ChangeName(string message)
    {
        if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeName(ChangeNameViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try
        {
            await _userService.ChangeNameAsync(User.Identity!.Name!, model.Name);
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

            ModelState.AddModelError("", text);
            return View(model);
        }
    }
}