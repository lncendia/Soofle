using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
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
    public IActionResult Index(string? message)
    {
        ViewData["Alert"] = message;
        return View();
    }

    public async Task<ActionResult> GetReports(int page = 1)
    {
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        var reports = await _reportManager.FindAsync()
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartParticipantReport()
    {
        
    }
    

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartLikeReport(StartLikeOrCommentReportViewModel model)
    {
           
    }
        
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RestartReport(Guid? id)
    {
        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteReport(Guid? id)
    {
        
    }
}