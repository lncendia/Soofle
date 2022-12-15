using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VkQ.WEB.ViewModels;

namespace VkQ.WEB.Controllers;

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
}