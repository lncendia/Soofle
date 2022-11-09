using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VkQ.WEB.ViewModels;

namespace VkQ.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            else if (!string.IsNullOrEmpty(_configuration["Message"]))
                ViewData["Alert"] = _configuration["Message"];
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}