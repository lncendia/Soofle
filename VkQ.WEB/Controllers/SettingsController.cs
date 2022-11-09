using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VkQ.WEB.ViewModels.Settings;

namespace VkQ.WEB.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly EmailService _emailService;
        private readonly CommunicationService _communicationLink;
        private readonly ApplicationDbContext _db;

        public SettingsController(UserManager<User> userManager, EmailService emailService,
            CommunicationService communicationLink, ApplicationDbContext db)
        {
            _userManager = userManager;
            _emailService = emailService;
            _communicationLink = communicationLink;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Communications(string message)
        {
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            var user = await _userManager.GetUserAsync(User);
            var data = new CommunicationsViewModel
            {
                Links = _communicationLink.GetCommunications(user),
                CurrentUser = user,
                Scheme = HttpContext.Request.Scheme
            };
            return View(data);
        }

        [HttpGet]
        public IActionResult CreateCommunication()
        {
            return View(new CreateCommunicationLinkViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCommunication(CreateCommunicationLinkViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (!(model.CommonReports || model.CommonThieves))
            {
                ModelState.AddModelError(String.Empty, "Выберите разрешения");
                return View(model);
            }

            User user = await _userManager.GetUserAsync(User);
            if (_db.CommunicationLinks.Count(link => link.Sender == user || link.Recipient == user) >= 10)
                return RedirectToAction("Communications", new {message = "Вы не можете создать более 10 связей."});
            _communicationLink.CreateCommunication(user, model.CommonThieves, model.CommonReports);
            return RedirectToAction("Communications", new {message = "Приглашение успешно создано."});
        }

        public async Task<IActionResult> DeleteCommunication(Guid id)
        {
            User user = await _userManager.GetUserAsync(User);
            var link = _db.CommunicationLinks.FirstOrDefault(communicationLink =>
                communicationLink.Id == id &&
                (communicationLink.Sender == user || communicationLink.Recipient == user));
            if (link == null) return RedirectToAction("Communications", new {message = "Приглашение не найдено."});
            _db.Remove(link);
            await _db.SaveChangesAsync();
            return RedirectToAction("Communications", new {message = "Связь удалена."});
        }

        [HttpGet]
        public async Task<IActionResult> Communication(Guid id)
        {
            User user = await _userManager.GetUserAsync(User);
            var link = _db.CommunicationLinks.FirstOrDefault(communicationLink =>
                communicationLink.Id == id && communicationLink.Sender != user);
            if (link == null) return RedirectToAction("Communications", new {message = "Приглашение не найдено."});
            return View(link);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptCommunication(Guid id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Communications");

            User user = await _userManager.GetUserAsync(User);

            if (_db.CommunicationLinks.Count(communicationLink =>
                communicationLink.Sender == user || communicationLink.Recipient == user) >= 10)
                return RedirectToAction("Communications", new {message = "Вы не можете создать более 10 связей."});

            var link = _db.CommunicationLinks.FirstOrDefault(communicationLink =>
                communicationLink.Id == id && communicationLink.Sender != user);
            if (link == null) return RedirectToAction("Communications", new {message = "Приглашение не найдено."});

            _communicationLink.AcceptCommunication(user, link);
            return RedirectToAction("Communications", new {message = "Приглашение успешно принято."});
        }

        [HttpGet]
        public async Task<IActionResult> ChangeEmail(string message)
        {
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            var user = await _userManager.GetUserAsync(User);
            return View(new ChangeEmailViewModel {Email = user.Email});
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            User user = await _userManager.GetUserAsync(User);
            if (model.Email == user.Email)
            {
                ModelState.AddModelError(String.Empty, "Вы уже используете эту почту");
                return View(model);
            }

            var code = await _userManager.GenerateChangeEmailTokenAsync(user, model.Email);
            var callbackUrl = Url.Action(
                "AcceptChangeEmail",
                "Settings",
                new {email = model.Email, code},
                HttpContext.Request.Scheme);
            _emailService.SendMessage(user.UserName, model.Email,
                $"Подтвердите смену почты, перейдя по <a href=\"{callbackUrl}\">ссылке</a>.");
            return RedirectToAction("ChangeEmail",
                new
                {
                    message = "Для завершения проверьте электронную почту и перейдите по ссылке, указанной в письме."
                });
        }

        [HttpGet]
        public async Task<IActionResult> AcceptChangeEmail(string email, string code)
        {
            var user = await _userManager.GetUserAsync(User);
            if (code == null || email == null)
            {
                return RedirectToAction("ChangeEmail", new
                {
                    message =
                        "Ссылка недействительна."
                });
            }

            var result = await _userManager.ChangeEmailAsync(user, email, code);
            if (result.Succeeded)
                return RedirectToAction("ChangeEmail", new
                {
                    message =
                        "Почта успешно изменена."
                });
            string errorMessage =
                result.Errors.Aggregate(String.Empty, (current, error) => current + "\n" + error.Description);

            return RedirectToAction("ChangeEmail",
                new {message = $"Ошибка: {errorMessage}."});
        }

        [HttpGet]
        public IActionResult ChangePassword(string message)
        {
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            if (model.OldPassword == model.Password)
            {
                ModelState.AddModelError(String.Empty, "Этот пароль уже используется.");
                return View(model);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
            if (result.Succeeded)
                return RedirectToAction("ChangePassword", new {message = "Пароль успешно изменен."});

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeTimeZone(string message)
        {
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            var user = await _userManager.GetUserAsync(User);
            return View(new SetTimeZoneViewModel() {Id = user.TimeZoneId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTimeZone(SetTimeZoneViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            user.TimeZoneId = model.Id;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction("ChangeTimeZone", new {message = "Часовой пояс успешно изменен."});

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}