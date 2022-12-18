using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.ReportProcessing;

namespace VkQ.WEB.Controllers;

[Authorize]
public class ReportsManagerController : Controller
{
    private readonly IReportCreationService _reportCreationService;


    public ReportsManagerController(IReportCreationService reportCreationService)
    {
        _reportCreationService = reportCreationService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartParticipantReport()
    {
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartLikeReport()
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
        //todo: check if linked report
    }
}