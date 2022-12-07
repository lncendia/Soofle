using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Reports.ServicesInterfaces;
using VkQ.WEB.ViewModels.Reports;

namespace VkQ.WEB.Controllers;

[Authorize]
public class ReportsController : Controller
{
    private readonly IReportManager _reportManager;

    public ReportsController(IReportManager reportManager)
    {
        _reportManager = reportManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetImage(string url)
    {
        var stream = await _service.GetImageAsync(url);
        string fileType = "application/jpg";
        string fileName = "img.jpg";
        return File(stream, fileType, fileName);
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? message, int page = 1)
    {
        ViewData["Alert"] = message;

        var model = new ReportsViewModel
        {
            Id = id,
            Count = _service.GetReportsCount(instagram),
            User = user
        };

        if ((page - 1) * 30 > model.Count) page = 1;
        model.Page = page;
        model.Reports = _service.GetReports(instagram, page);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> LikeReport(MediaReportViewModel model)
    {
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ParticipantsReport(ParticipantReportViewModel model)
    {
          
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> StartParticipantReport(int id)
    {
        return RedirectToAction("Reports", new {id, message = "Отчет успешно создан."});
    }
        

    [HttpGet]
    public async Task<IActionResult> StartLikeReport(int id)
    {
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartLikeReport(StartLikeOrCommentReportViewModel model)
    {
           
    }
        
    [HttpGet]
    public async Task<IActionResult> RestartReport(int id)
    {
           
        return RedirectToAction("Reports",
            new
            {
                report.Instagrams.First().Id,
                message = success.Succeeded
                    ? "Отчет успешно создан."
                    : $"Не удалось создать отчёт ({success.Message})."
            });
    }

    [HttpGet]
    public async Task<IActionResult> DeleteReport(int id)
    {
           
        if (!stop)
            return RedirectToAction("Reports",
                new
                {
                    id = instagramId,
                    message = "Не удалось остановить отчёт."
                });
        var result = _service.DeleteReport(report);
        return RedirectToAction("Reports",
            new
            {
                id = instagramId,
                message = result.Succeeded && result.Value
                    ? "Отчёт успешно удалён."
                    : $"Не удалось удалить отчёт ({result.Message})."
            });
    }
}