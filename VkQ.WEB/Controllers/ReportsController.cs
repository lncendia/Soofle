using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.ReportsQuery.DTOs;
using VkQ.Application.Abstractions.ReportsQuery.Exceptions;
using VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.WEB.ViewModels.Reports;
using IReportMapper = VkQ.WEB.Mappers.Abstractions.IReportMapper;

namespace VkQ.WEB.Controllers;

[Authorize]
public class ReportsController : Controller
{
    private readonly IReportManager _reportManager;
    private readonly IReportMapper _reportMapper;

    public ReportsController(IReportManager reportManager, IReportMapper reportMapper)
    {
        _reportManager = reportManager;
        _reportMapper = reportMapper;
    }

    [HttpGet]
    public IActionResult Index(string? message)
    {
        ViewData["Alert"] = message;
        return View();
    }

    [HttpGet]
    public async Task<ActionResult> GetReports(ReportsSearchQueryViewModel search)
    {
        if (!ModelState.IsValid) search = new ReportsSearchQueryViewModel();
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        try
        {
            var reports = await _reportManager.FindAsync(id,
                new SearchQuery(search.Page, search.ReportType, search.Hashtag, search.From, search.To));
            return PartialView("ReportsList", reports.Select(Map));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                UserNotFoundException => "Пользователь не найден",
                _ => "Произошла ошибка"
            };
            return RedirectToAction("Index", new { message });
        }
    }

    private static ReportShortViewModel Map(ReportShortDto dto) =>
        new(dto.Id, dto.Hashtag, dto.Type, dto.CreationDate, dto.EndDate, dto.IsCompleted, dto.IsSucceeded);


    [HttpGet]
    public async Task<IActionResult> LikeReport(Guid? id)
    {
        if (!id.HasValue) return RedirectToAction("Index", new { message = "Отчёт не найден" });
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        try
        {
            var report = await _reportManager.GetLikeReportAsync(userId, id.Value);
            ViewBag["Chats"] = report.LinkedUsers;
            return View(_reportMapper.LikeReportMapper.Value.Map(report));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ReportNotFoundException => "Отчет не найден",
                _ => "Произошла ошибка"
            };
            return RedirectToAction("Index", new { message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ParticipantsReport(Guid? id)
    {
        if (!id.HasValue) return RedirectToAction("Index", new { message = "Отчёт не найден" });
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        try
        {
            var report = await _reportManager.GetParticipantReportAsync(userId, id.Value);
            return View(_reportMapper.ParticipantReportMapper.Value.Map(report));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ReportNotFoundException => "Отчет не найден",
                _ => "Произошла ошибка"
            };
            return RedirectToAction("Index", new { message });
        }
    }
}