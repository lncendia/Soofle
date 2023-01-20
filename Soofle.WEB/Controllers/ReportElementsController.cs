using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soofle.Application.Abstractions.Elements.DTOs;
using Soofle.Application.Abstractions.Elements.DTOs.PublicationElementDto;
using Soofle.Application.Abstractions.Elements.ServicesInterfaces;
using Soofle.Application.Abstractions.ReportsQuery.Exceptions;
using Soofle.WEB.ViewModels.Elements;
using IElementMapper = Soofle.WEB.Mappers.Abstractions.IElementMapper;

namespace Soofle.WEB.Controllers;

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
            var publicationsViewModels = elements.Publications.Select(Map).ToList();
            var elementsViewModels = elements.Elements.Select(x => _elementMapper.LikeElementMapper.Value.Map(x));
            return elements.Elements.Any()
                ? PartialView("LikeElementsList", new LikeElementsViewModel(elementsViewModels, publicationsViewModels))
                : BadRequest();
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
    public async Task<IActionResult> CommentReportElements(PublicationElementsSearchQueryViewModel query)
    {
        if (!ModelState.IsValid) return BadRequest();
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var elements = await _elementManager.GetCommentReportElementsAsync(userId, query.ReportId,
                new PublicationElementSearchQuery(query.Page, query.Name, query.Succeeded, query.LikeChatName,
                    query.HasChildren, query.Vip));
            var publicationsViewModels = elements.Publications.Select(Map).ToList();
            var elementsViewModels = elements.Elements.Select(x => _elementMapper.CommentElementMapper.Value.Map(x));
            return elements.Elements.Any()
                ? PartialView("CommentElementsList", new CommentElementsViewModel(elementsViewModels, publicationsViewModels))
                : BadRequest();
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
    public async Task<IActionResult> ParticipantReportElements(ParticipantElementsSearchQueryViewModel query)
    {
        if (!ModelState.IsValid) return BadRequest();
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var elements = await _elementManager.GetParticipantReportElementsAsync(userId, query.ReportId,
                new ParticipantElementSearchQuery(query.Page, query.Name, query.ElementType, query.HasChildren,
                    query.ParticipantType));
            return elements.Any()
                ? PartialView("ParticipantElementsList",
                    elements.Select(x => _elementMapper.ParticipantElementMapper.Value.Map(x)))
                : BadRequest();
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


    private static PublicationViewModel Map(PublicationDto publication) => new(publication.Id,
        publication.ItemId, publication.OwnerId, publication.IsLoaded);
}