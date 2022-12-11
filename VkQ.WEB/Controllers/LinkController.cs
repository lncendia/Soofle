using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Links.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Domain.Links.Exceptions;
using VkQ.WEB.ViewModels.Profile;

namespace VkQ.WEB.Controllers;

[Authorize]
public class LinkController : Controller
{
    private readonly ILinkManager _linkManager;

    public LinkController(ILinkManager linkManager)
    {
        _linkManager = linkManager;
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateLink(CreateLinkViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        try
        {
            var dto = await _linkManager.AddAsync(id, model.Email);
            return Json(new { Id1 = id, Name1 = User.Identity!.Name, Id2 = dto.Id, Name2 = dto.User2Name });
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь с таким email не найден",
                SameUsersException => "Нельзя добавить самого себя",
                TooManyLinksForUserException => "Нельзя добавить больше 20 пользователей",
                _ => "Произошла ошибка при создании связи"
            };
            return BadRequest(text);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteLink(Guid? id)
    {
        if (!id.HasValue) return BadRequest("Id is null");
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        try
        {
            await _linkManager.DeleteAsync(userId, id.Value);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь с таким email не найден",
                _ => "Произошла ошибка при удалении связи"
            };
            return BadRequest(text);
        }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AcceptLink(Guid? id)
    {
        if (!id.HasValue) return BadRequest("Id is null");
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        try
        {
            await _linkManager.AcceptAsync(userId, id.Value);
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                UserNotFoundException => "Пользователь с таким email не найден",
                TooManyLinksForUserException => "Нельзя добавить больше 20 пользователей",
                _ => "Произошла ошибка при создании связи"
            };
            return BadRequest(text);
        }

        return Ok();
    }
}