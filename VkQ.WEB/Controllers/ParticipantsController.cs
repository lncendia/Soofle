using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VkQ.WEB.ViewModels.Participants;

namespace VkQ.WEB.Controllers
{
    [Authorize]
    public class ParticipantsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ParticipantsService _participantsService;
        private readonly ApplicationDbContext _db;

        public ParticipantsController(ParticipantsService participantsService, UserManager<User> userManager,
            ApplicationDbContext db)
        {
            _participantsService = participantsService;
            _userManager = userManager;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> SelectChat(string message)
        {
            var x = _db.Participants
                .Where(participant => _db.Participants.Any(participant1 => participant1.Pk == participant.Pk)).ToList();
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            var user = await _userManager.GetUserAsync(User);
            return View(_db.Instagrams.Where(instagram => instagram.User == user).ToList());
        }


        [HttpGet]
        public async Task<IActionResult> MyParticipants(ParticipantsViewModel model)
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
        public async Task<IActionResult> ParticipantsFromNicks(int id)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new { message = "Неверный ID." });
            var user = await _userManager.GetUserAsync(User);
            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == id && instagram1.User == user);
            if (instagram == null) return RedirectToAction("SelectChat", new { message = "Аккаунт не найден." });
            return View(new ParticipantsFromNicksViewModel() { Id = instagram.Id });
        }

        [HttpPost]
        public async Task<IActionResult> ParticipantsFromNicks(ParticipantsFromNicksViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == model.Id && instagram1.User == user);
            if (instagram == null) return RedirectToAction("SelectChat", new { message = "Аккаунт не найден." });
            model.Participants = _participantsService.GetParticipants(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Participant(int id, string message)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new { message = "Неверный ID." });
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            var user = await _userManager.GetUserAsync(User);
            var participant =
                _db.Participants.Include(participant1 => participant1.ParentParticipant)
                    .Include(participant1 => participant1.Instagram).FirstOrDefault(participant1 =>
                        participant1.Instagram.User == user && participant1.Id == id);
            if (participant == null)
                return RedirectToAction("SelectChat", new { message = "Участник не найден." });
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
}