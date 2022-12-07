using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Links.ServicesInterfaces;
using VkQ.WEB.ViewModels.Profile;

namespace VkQ.WEB.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly ILinkManager _linkManager;

    public ProfileController(ILinkManager linkManager) => _linkManager = linkManager;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        var links = await _linkManager.GetLinksAsync(id);
        var linksViewModel = links.Select(dto =>
            new LinksViewModel.LinkViewModel(dto.Id, dto.User1, dto.User2, dto.IsConfirmed));
        var data = new LinksViewModel(id, HttpContext.Request.Scheme, linksViewModel.ToList());
        var model = new ProfileViewModel();
        return View(data);
    }
}