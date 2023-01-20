using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Soofle.WEB.ViewModels;

namespace Soofle.WEB.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string? message)
    {
        ViewData["Alert"] = message;
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}