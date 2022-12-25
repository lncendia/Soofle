using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Elements.DTOs;
using VkQ.Application.Abstractions.Elements.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsQuery.Exceptions;
using VkQ.WEB.ViewModels.Elements;
using IElementMapper = VkQ.WEB.Mappers.Abstractions.IElementMapper;

namespace VkQ.WEB.Controllers;

[Authorize]
public class ReportElementsController : Controller
{
    private readonly IReportElementManager _elementManager;
    private readonly IElementMapper _elementMapper;

    public ReportElementsController(IReportElementManager elementManager, IElementMapper elementMapper)
    {
        _elementManager = elementManager;
        _elementMapper = elementMapper;
    }

    // [HttpGet]
    // public async Task<IActionResult> GetImage(string url)
    // {
    //     var stream = await _service.GetImageAsync(url);
    //     const string fileType = "application/jpg";
    //     const string fileName = "img.jpg";
    //     return File(stream, fileType, fileName);
    // }

    [HttpGet]
    public async Task<IActionResult> LikeReportElements(PublicationElementsSearchQueryViewModel query)
    {
        if (!ModelState.IsValid) return BadRequest();
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var elements = await _elementManager.GetLikeReportElementsAsync(userId, query.ReportId,
                new PublicationElementSearchQuery(query.Page, query.Name, query.Succeeded, query.LikeChatName,
                    query.HasChildren, query.Vip));
            return PartialView("LikeElementsList", elements.Select(x => _elementMapper.LikeElementMapper.Value.Map(x)));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ReportNotFoundException => "Отчёт не найден",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> ParticipantsReportElements(ParticipantElementsSearchQueryViewModel query)
    {
        if (!ModelState.IsValid) return BadRequest();
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var elements = await _elementManager.GetParticipantReportElementsAsync(userId, query.ReportId,
                new ParticipantElementSearchQuery(query.Page, query.Name, query.ElementType, query.HasChildren));
            return PartialView("ParticipantElementsList",
                elements.Select(x => _elementMapper.ParticipantElementMapper.Value.Map(x)));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ReportNotFoundException => "Отчёт не найден",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }
    }
}