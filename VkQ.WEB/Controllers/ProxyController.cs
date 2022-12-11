using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Proxies.ServicesInterfaces;
using VkQ.WEB.ViewModels.Proxy;

namespace VkQ.WEB.Controllers;

[Authorize(Roles = "admin")]
public class ProxyController : Controller
{
    private readonly IProxyManager _proxyManager;

    public ProxyController(IProxyManager proxyManager) => _proxyManager = proxyManager;

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Proxies(int page = 1, string? host = null)
    {
        var proxies = await _proxyManager.FindAsync(page, host);
        return Json(proxies.Select(dto => new ProxyViewModel(dto.Id, dto.Host, dto.Port, dto.Login, dto.Password)));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProxy(Guid? id)
    {
        if (!id.HasValue) return BadRequest("Id is null");
        await _proxyManager.DeleteAsync(id.Value);
        return Ok();
    }
}