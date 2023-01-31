using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soofle.Application.Abstractions.Proxies.Exceptions;
using Soofle.Application.Abstractions.Proxies.ServicesInterfaces;
using Soofle.WEB.ViewModels.Proxy;

namespace Soofle.WEB.Controllers;

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
    public async Task<IActionResult> Proxies(ProxySearchQueryViewModel model)
    {
        var proxies = await _proxyManager.FindAsync(model.Page, model.Host);
        return proxies.Any()
            ? PartialView("Proxies",
                proxies.Select(x => new ProxyViewModel(x.Id, x.Host, x.Port, x.Login, x.Password)))
            : BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (!id.HasValue) return BadRequest("Id is null");
        try
        {
            await _proxyManager.DeleteAsync(id.Value);
            return Ok();
        }
        catch
        {
            return BadRequest("Не удалось удалить прокси");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddProxyViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        try
        {
            await _proxyManager.AddAsync(model.ProxyList);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                ProxyListParseException exception => $"Неверные данные (строка {exception.Line}",
            _ => "Произошла ошибка при добавлении прокси"
            };
            return BadRequest(text);
        }
    }
}