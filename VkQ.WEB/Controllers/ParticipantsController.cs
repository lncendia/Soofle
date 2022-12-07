using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Participants.ServicesInterfaces;
using VkQ.WEB.ViewModels.Participants;

namespace VkQ.WEB.Controllers;

[Authorize]
public class ParticipantsController : Controller
{
    private readonly IParticipantService _participantsService;
    
    public ParticipantsController(IParticipantService participantsService) => _participantsService = participantsService;


    [HttpGet]
    public async Task<IActionResult> Index(string? message)
    {
        if (!ModelState.IsValid) return RedirectToAction("SelectChat");
        var user = await _userManager.GetUserAsync(User);
        if (!_db.Instagrams.Any(instagram1 =>
                instagram1.User == user && instagram1.Id == model.Id))
            return RedirectToAction("SelectChat", new { message = "Аккаунт не найден." });
        model.Count = _participantsService.GetParticipantsCount(model);
        if ((model.Page - 1) * 30 > model.Count) model.Page = 1;
        model.Participants = _participantsService.GetParticipants(model);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Participant(Guid? id)
    {
        var user = await _userManager.GetUserAsync(User);
        var participant =
            _db.Participants.Include(participant1 => participant1.ParentParticipant)
                .Include(participant1 => participant1.Instagram).FirstOrDefault(participant1 =>
                    participant1.Instagram.User == user && participant1.Id == id);
        var model = new EditParticipantViewModel
        {
            Id = participant.Id,
            Vip = participant.Vip,
            Note = participant.Note,
            Username = participant.Username,
            Participants = _participantsService.GetParticipantsSelectList(participant)
        };
        if (participant.ParentParticipant != null) model.ParentId = participant.ParentParticipant.Id;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Participant(EditParticipantViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        var participant =
            _db.Participants.Include(participant1 => participant1.ParentParticipant)
                .Include(participant1 => participant1.Instagram).FirstOrDefault(participant1 =>
                    participant1.Id == model.Id &&
                    participant1.Instagram.User == user);
        if (participant == null)
            return RedirectToAction("SelectChat", new { message = "Участник не найден." });
        if (!ModelState.IsValid)
        {
            model.Participants = _participantsService.GetParticipantsSelectList(participant);
            return View(model);
        }

        var success = _participantsService.EditParticipant(participant, model);
        return RedirectToAction("Participant",
            new
            {
                id = model.Id,
                message = success.Succeeded && success.Value
                    ? "Участник успешно изменен."
                    : $"Не удалось изменить данные участника ({success.Message})."
            });
    }
}