using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Links.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Application.Abstractions.Vk.ServicesInterfaces;
using VkQ.WEB.ViewModels.Profile;
using VkQ.WEB.ViewModels.Vk;

namespace VkQ.WEB.Controllers;

[Authorize]
public class VkController : Controller
{
    private readonly IVkManager _vkManager;

    public VkController(IVkManager vkManager) => _vkManager = vkManager;

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetVk(SetVkViewModel model)
    {
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        try
        {
            await _vkManager.SetVkAsync(id, model.Login, model.Password);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь не найден",
                _ => "Произошла ошибка при добавлении Vk"
            };
            return BadRequest(text);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ActivateVk()
    {
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        await _vkManager.ActivateVkAsync(id, null);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TwoFactorVk(TwoFactorViewModel model)
    {
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        await _vkManager.ActivateVkAsync(id, model.Code);
    }
}