using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Soofle.Application.Abstractions.Users.Entities;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.Application.Abstractions.Users.ServicesInterfaces;
using Soofle.Domain.Users.Exceptions;

namespace Soofle.WEB.Controllers;

[Authorize]
public class VkController : Controller
{
    private readonly IVkManager _vkManager;
    private readonly SignInManager<UserData> _manager;

    public VkController(IVkManager vkManager, SignInManager<UserData> manager)
    {
        _vkManager = vkManager;
        _manager = manager;
    }


    public IActionResult Login()
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Vk");
        var properties = _manager.ConfigureExternalAuthenticationProperties("Vk", redirectUrl);
        return new ChallengeResult("Vk", properties);
    }

    public async Task<IActionResult> ExternalLoginCallback()
    {
        var info = await _manager.GetExternalLoginInfoAsync();
        if (info == null) return RedirectToAction(nameof(Login));
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            await _vkManager.SetAsync(userId, info);
            await HttpContext.SignOutAsync(info.AuthenticationProperties);
            return RedirectToAction("Index", "Home", new { message = "Вк успешно установлен" });
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                InvalidEmailException => "Неверный формат почты",
                _ => "Произошла ошибка при входе"
            };
            return RedirectToAction("Index", "Home", new { message = text });
        }
    }
}