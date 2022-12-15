using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Elements.ServicesInterfaces;
using VkQ.WEB.ViewModels.Reports;

namespace VkQ.WEB.Controllers;

[Authorize]
public class ReportElementsController : Controller
{
    private readonly IReportElementManager _reportManager;

    public ReportElementsController(IReportElementManager reportManager)
    {
        _reportManager = reportManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetImage(string url)
    {
        var stream = await _service.GetImageAsync(url);
        const string fileType = "application/jpg";
        const string fileName = "img.jpg";
        return File(stream, fileType, fileName);
    }

    [HttpGet]
    public async Task<IActionResult> LikeReportElements(MediaReportViewModel model)
    {
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ParticipantsReportElements(ParticipantReportViewModel model)
    {
          
        return View(model);
    }
}