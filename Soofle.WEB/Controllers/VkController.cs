using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soofle.Application.Abstractions.Proxies.Exceptions;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.Application.Abstractions.Vk.Exceptions;
using Soofle.Application.Abstractions.Vk.ServicesInterfaces;
using Soofle.WEB.ViewModels.Vk;
using Soofle.Domain.Users.Exceptions;

namespace Soofle.WEB.Controllers;

[Authorize]
public class VkController : Controller
{
    private readonly IVkManager _vkManager;

    public VkController(IVkManager vkManager) => _vkManager = vkManager;

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Change(SetVkViewModel model)
    {
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            await _vkManager.SetAsync(id, model.Login, model.Password);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                UnableFindProxyException => "Не удалось найти подходящий прокси",
                _ => "Произошла ошибка при добавлении Vk"
            };
            return BadRequest(text);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Activate(TwoFactorViewModel model)
    {
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            if (string.IsNullOrEmpty(model.Code)) await _vkManager.ActivateAsync(id);
            else await _vkManager.ActivateTwoFactorAsync(id, model.Code);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                InvalidCredentialsException => "Неверный логин или пароль",
                TwoFactorRequiredException => "Укажите код двухфакторной авторизации",
                UnableFindProxyException => "Не удалось найти подходящий прокси",
                VkIsNotSetException => "Не установлен Vk",
                ErrorActiveVkException=>"Ошибка при отправке запроса",
                _ => "Произошла ошибка при активации Vk"
            };
            return BadRequest(text);
        }
    }
}