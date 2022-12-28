using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Links.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsManagement.DTOs;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.ReportProcessing;
using VkQ.Domain.Reposts.BaseReport.Exceptions;
using VkQ.Domain.Reposts.ParticipantReport.Exceptions;
using VkQ.Domain.Reposts.PublicationReport.Exceptions;
using VkQ.WEB.ViewModels.ReportsManager;

namespace VkQ.WEB.Controllers;

[Authorize]
public class ReportsManagerController : Controller
{
    private readonly IReportCreationService _reportCreationService;
    private readonly IUserLinksService _userLinksService;

    public ReportsManagerController(IReportCreationService reportCreationService, IUserLinksService userLinksService)
    {
        _reportCreationService = reportCreationService;
        _userLinksService = userLinksService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var links = await _userLinksService.GetUserLinksAsync(userId);
        ViewBag.Links = links;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartParticipantReport()
    {
        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _reportCreationService.CreateParticipantReportAsync(userId);
        }
        catch (Exception e)
        {
            var message = e switch
            {
                UserChatIdException => "Отчёт не найден",
                UserSubscribeException => "Продлите подписку",
                UserVkException => "ВК не активирован",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }

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
        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _reportCreationService.CreateLikeReportAsync(
                new PublicationReportCreateDto(userId, model.Hashtag, model.SearchStartDate, model.CoAuthors));
            return Ok();
        }
        catch (Exception e)
        {
            var message = e switch
            {
                UserSubscribeException => "Продлите подписку",
                UserVkException => "ВК не активирован",
                TooManyLinksException => "Кол-во связей не должно превышать 20 единиц",
                ArgumentException => "Связь не активирована",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }
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