using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.ReportsManagement.DTOs;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.ReportProcessing;
using VkQ.WEB.ViewModels.ReportsManager;

namespace VkQ.WEB.Controllers;

[Authorize]
public class ReportsManagerController : Controller
{
    private readonly IReportCreationService _reportCreationService;

    public ReportsManagerController(IReportCreationService reportCreationService) =>
        _reportCreationService = reportCreationService;

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartParticipantReport()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        await _reportCreationService.CreateParticipantReportAsync(new ParticipantReportCreateDto(userId, 111));
        return Ok();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartLikeReport(PublicationReportCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        await _reportCreationService.CreateLikeReportAsync(
            new PublicationReportCreateDto(userId, model.Hashtag, model.SearchStartDate, model.CoAuthors));
        return Ok();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RestartReport(Guid? id)
    {
        return Ok();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteReport(Guid? id)
    {
        //todo: check if linked report
        return Ok();
    }
}