using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Participants.DTOs;
using VkQ.Application.Abstractions.Participants.Exceptions;
using VkQ.Application.Abstractions.Participants.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.WEB.ViewModels.Participants;

namespace VkQ.WEB.Controllers;

[Authorize]
public class ParticipantsController : Controller
{
    private readonly IParticipantManager _participantManager;
    private readonly IUserParticipantsService _userParticipants;

    public ParticipantsController(IParticipantManager participantManager, IUserParticipantsService userParticipants)
    {
        _participantManager = participantManager;
        _userParticipants = userParticipants;
    }


    [HttpGet]
    public IActionResult Index(string? message)
    {
        ViewData["Alert"] = message;
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> Participants(ParticipantsSearchQueryViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        try
        {
            var participants = await _participantManager.FindAsync(userId,
                new SearchQuery(model.Page, model.Username, model.Type, model.Vip, model.HasChild));
            return PartialView("ParticipantsList", participants.Select(Map));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                UserNotFoundException => "Пользователь не найден",
                _ => "Произошла ошибка"
            };
            return BadRequest(message);
        }
    }


    private static ParticipantViewModel Map(ParticipantDto participant) =>
        new(participant.Id, participant.Name, participant.Notes, participant.Vip,
            participant.Type, participant.Children.Select(Map));


    [HttpGet]
    public async Task<IActionResult> EditParticipant(Guid? id)
    {
        if (!id.HasValue) return RedirectToAction("Index", new { message = "Участник не найден" });
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        try
        {
            var participant = await _participantManager.GetAsync(userId, id.Value);
            var participants = await _userParticipants.GetUserParticipantsAsync(userId);
            ViewBag["Participants"] = participants;
            return View(new EditParticipantViewModel
            {
                Id = participant.Id,
                Note = participant.Notes,
                ParentId = participant.ParentParticipantId,
                Username = participant.Name,
                Vip = participant.Vip,
                Type = participant.Type
            });
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ParticipantNotFoundException => "Участник не найден",
                _ => "Произошла ошибка"
            };
            return RedirectToAction("Index", new { message });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditParticipant(EditParticipantViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        try
        {
            await _participantManager.EditAsync(userId, model.Id, model.ParentId, model.Note, model.Vip);
            return RedirectToAction("Index", new { message = "Участник успешно изменен" });
        }
        catch (Exception e)
        {
            var message = e switch
            {
                ParticipantNotFoundException => "Участник не найден",
                _ => "Произошла ошибка"
            };
            return RedirectToAction("Index", new { message });
        }
    }
}