using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VkQ.Domain.Users.Entities;
using VkQ.WEB.ViewModels.AccountsInstagram;

namespace VkQ.WEB.Controllers
{
    [Authorize]
    public class AccountsInstagramController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly InstagramLoginService _loginService;

        public AccountsInstagramController(UserManager<User> userManager, ApplicationDbContext db,
            InstagramLoginService loginService)
        {
            _userManager = userManager;
            _db = db;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> Accounts(string message)
        {
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            var user = await _userManager.GetUserAsync(User);
            return View(_db.Instagrams.Where(instagram => instagram.User == user).ToList());
        }

        [HttpGet]
        public IActionResult AddAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccount(InstagramLoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (_db.Instagrams.Count(instagram => instagram.User == user) >= 20)
                return RedirectToAction("Accounts",
                    new
                    {
                        message = "Вы не можете добавить более 20 аккаунтов."
                    });
            var success = _loginService.AddInstagram(model, user);
            if (success)
                return RedirectToAction("Accounts",
                    new
                    {
                        message = "Аккаунт успешно добавлен."
                    });

            ModelState.AddModelError("", "Вы уже добавили этот аккаунт.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Activate(int id)
        {
            if (id <= 0) return RedirectToAction("Accounts");
            var user = await _userManager.GetUserAsync(User);
            var instagram =
                _db.Instagrams.Include(instagram1 => instagram1.Proxy).FirstOrDefault(instagram1 =>
                    instagram1.Id == id && instagram1.User == user && instagram1.IsActivated == false);
            if (instagram == null) return RedirectToAction("Accounts");
            var response = await _loginService.ActivateAsync(instagram);
            switch (response.Value)
            {
                case InstaLoginResult.Success:
                {
                    if (!response.Succeeded) break;
                    await _loginService.SendRequestsAfterLoginAsync(instagram);
                    instagram.IsActivated = true;
                    await _db.SaveChangesAsync();
                    return RedirectToAction("StartParticipantReport", "Reports",
                        new
                        {
                            id = instagram.Id
                        });
                }
                case InstaLoginResult.TwoFactorRequired:
                    return RedirectToAction("TwoFactorRequired", new { id = instagram.Id });
                case InstaLoginResult.ChallengeRequired:
                {
                    var challenge = await _loginService.GetChallengeAsync(instagram);
                    if (!challenge.Succeeded)
                        return RedirectToAction("Accounts",
                            new
                            {
                                message =
                                    $"Произошла ошибка ({challenge.Info.Message}). Проверьте корректность данных и попробуйте ещё раз."
                            });


                    if (challenge.Value.SubmitPhoneRequired)
                        return RedirectToAction("SubmitPhoneNumber", new { id = instagram.Id });

                    if (string.IsNullOrEmpty(challenge.Value.StepData.PhoneNumber))
                    {
                        var response2 = await _loginService.EmailMethodChallengeRequiredAsync(instagram);
                        if (response2.Succeeded)
                            return RedirectToAction("ChallengeRequired",
                                new { id = instagram.Id, contact = response2.Value.StepData.ContactPoint });
                    }

                    else if (string.IsNullOrEmpty(challenge.Value.StepData.Email))
                    {
                        var response2 = await _loginService.SmsMethodChallengeRequiredAsync(instagram);
                        if (response2.Succeeded)
                            return RedirectToAction("ChallengeRequired",
                                new { id = instagram.Id, contact = response2.Value.StepData.ContactPoint });
                    }
                    else
                        return RedirectToAction("SelectMethodChallengeRequired", new { id = instagram.Id });

                    break;
                }
                case InstaLoginResult.BadPassword:
                    return RedirectToAction("Accounts",
                        new
                        {
                            message =
                                "Неверный пароль. Проверьте корректность данных и попробуйте ещё раз."
                        });
                case InstaLoginResult.InvalidUser:
                    return RedirectToAction("Accounts",
                        new
                        {
                            message =
                                "Пользователь не найден. Проверьте корректность данных и попробуйте ещё раз."
                        });
                case InstaLoginResult.LimitError:
                    return RedirectToAction("Accounts",
                        new
                        {
                            message =
                                "Слишком много запросов. Подождите несколько минут и попробуйте снова."
                        });
            }

            return RedirectToAction("Accounts",
                new
                {
                    message =
                        $"Произошла ошибка ({response.Info.Message}). Проверьте корректность данных и попробуйте ещё раз."
                });
        }

        [HttpGet]
        public IActionResult TwoFactorRequired(int id)
        {
            if (id <= 0) return RedirectToAction("Accounts");
            return View(new TwoFactorViewModel { Id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TwoFactorRequired(TwoFactorViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            var instagram =
                _db.Instagrams.Include(instagram1 => instagram1.Proxy).FirstOrDefault(instagram1 =>
                    instagram1.Id == model.Id && instagram1.User == user && instagram1.IsActivated == false &&
                    !string.IsNullOrEmpty(instagram1.StateData) &&
                    !string.IsNullOrEmpty(instagram1.TwoFactorLoginInfo));
            if (instagram == null) return RedirectToAction("Accounts");
            var response = await _loginService.EnterTwoFactorAsync(instagram, model.Code);
            ViewData["Title"] = instagram.Username;
            switch (response.Value)
            {
                case InstaLoginTwoFactorResult.Success:
                {
                    if (!response.Succeeded) break;

                    instagram.IsActivated = true;
                    await _loginService.SendRequestsAfterLoginAsync(instagram);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("StartParticipantReport", "Reports",
                        new
                        {
                            id = instagram.Id
                        });
                }
                case InstaLoginTwoFactorResult.InvalidCode:
                    ModelState.AddModelError("", "Код введен неверно.");
                    return View(model);
                case InstaLoginTwoFactorResult.CodeExpired:
                    ModelState.AddModelError("", "Время действия кода истекло. Попробуйте ещё раз.");
                    return View(model);
                case InstaLoginTwoFactorResult.ChallengeRequired:
                {
                    var challenge = await _loginService.GetChallengeAsync(instagram);
                    if (!challenge.Succeeded)
                        return RedirectToAction("Accounts",
                            new
                            {
                                message =
                                    $"Произошла ошибка ({challenge.Info.Message}). Проверьте корректность данных и попробуйте ещё раз."
                            });


                    if (challenge.Value.SubmitPhoneRequired)
                        return RedirectToAction("SubmitPhoneNumber", new { id = instagram.Id });

                    if (string.IsNullOrEmpty(challenge.Value.StepData.PhoneNumber))
                    {
                        var response2 = await _loginService.EmailMethodChallengeRequiredAsync(instagram);
                        if (response2.Succeeded)
                            return RedirectToAction("ChallengeRequired",
                                new { id = instagram.Id, contact = response2.Value.StepData.ContactPoint });
                    }

                    else if (string.IsNullOrEmpty(challenge.Value.StepData.Email))
                    {
                        var response2 = await _loginService.SmsMethodChallengeRequiredAsync(instagram);
                        if (response2.Succeeded)
                            return RedirectToAction("ChallengeRequired",
                                new { id = instagram.Id, contact = response2.Value.StepData.ContactPoint });
                    }
                    else
                        return RedirectToAction("SelectMethodChallengeRequired", new { id = instagram.Id });

                    break;
                }
            }

            return RedirectToAction("Accounts",
                new
                {
                    message =
                        $"Произошла ошибка ({response.Info.Message}). Проверьте корректность данных и попробуйте ещё раз."
                });
        }

        [HttpGet]
        public IActionResult SubmitPhoneNumber(int id)

        {
            if (id <= 0) return RedirectToAction("Accounts");
            return View(new SubmitPhoneViewModel { Id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitPhoneNumber(SubmitPhoneViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            var instagram =
                _db.Instagrams.Include(instagram1 => instagram1.Proxy).FirstOrDefault(instagram1 =>
                    instagram1.Id == model.Id && instagram1.User == user && instagram1.IsActivated == false &&
                    !string.IsNullOrEmpty(instagram1.StateData) &&
                    !string.IsNullOrEmpty(instagram1.ChallengeLoginInfo));
            if (instagram == null) return RedirectToAction("Accounts");
            var response = await _loginService.SubmitPhoneNumberAsync(instagram, model.PhoneNumber);
            if (response.Succeeded)
                return RedirectToAction("ChallengeRequired", new { id = instagram.Id, contact = model.PhoneNumber });
            ModelState.AddModelError("",
                $"Произошла ошибка ({response.Info.Message}). Попробуйте ещё раз.");
            return View(model);
        }

        [HttpGet]
        public IActionResult SelectMethodChallengeRequired(int id)
        {
            if (id <= 0) return RedirectToAction("Accounts");
            return View(new SelectMethodChallengeRequiredViewModel { Id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectMethodChallengeRequired(SelectMethodChallengeRequiredViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            var instagram =
                _db.Instagrams.Include(instagram1 => instagram1.Proxy).FirstOrDefault(instagram1 =>
                    instagram1.Id == model.Id && instagram1.User == user && instagram1.IsActivated == false &&
                    !string.IsNullOrEmpty(instagram1.StateData) &&
                    !string.IsNullOrEmpty(instagram1.ChallengeLoginInfo));
            if (instagram == null) return RedirectToAction("Accounts");

            string errorMessage = String.Empty;
            switch (model.Type)
            {
                case ChallengeType.Sms:
                    var response1 = await _loginService.SmsMethodChallengeRequiredAsync(instagram);
                    if (response1.Succeeded)
                        return RedirectToAction("ChallengeRequired",
                            new { id = instagram.Id, contact = response1.Value.StepData.ContactPoint });
                    errorMessage = response1.Info.Message;
                    break;
                case ChallengeType.Mail:
                    var response2 = await _loginService.EmailMethodChallengeRequiredAsync(instagram);
                    if (response2.Succeeded)
                        return RedirectToAction("ChallengeRequired",
                            new { id = instagram.Id, contact = response2.Value.StepData.ContactPoint });
                    errorMessage = response2.Info.Message;
                    break;
            }

            ModelState.AddModelError("",
                $"Произошла ошибка ({errorMessage}). Попробуйте ещё раз.");
            return View(model);
        }

        [HttpGet]
        public IActionResult ChallengeRequired(int id, string contact = null)

        {
            if (id == 0) return RedirectToAction("Accounts");
            return View(new ChallengeRequiredViewModel { Id = id, Contact = contact });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChallengeRequired(ChallengeRequiredViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            var instagram =
                _db.Instagrams.Include(instagram1 => instagram1.Proxy).FirstOrDefault(instagram1 =>
                    instagram1.Id == model.Id && instagram1.User == user && instagram1.IsActivated == false &&
                    !string.IsNullOrEmpty(instagram1.StateData) &&
                    !string.IsNullOrEmpty(instagram1.ChallengeLoginInfo));
            if (instagram == null) return RedirectToAction("Accounts");
            var response = await _loginService.SubmitChallengeAsync(instagram, model.Code);
            if (response.Succeeded && response.Value == InstaLoginResult.Success)
            {
                instagram.IsActivated = true;
                await _loginService.SendRequestsAfterLoginAsync(instagram);
                await _db.SaveChangesAsync();
                return RedirectToAction("StartParticipantReport", "Reports",
                    new
                    {
                        id = instagram.Id
                    });
            }

            ModelState.AddModelError("",
                $"Произошла ошибка ({response.Info.Message}). Попробуйте ещё раз.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return RedirectToAction("Accounts");
            var user = await _userManager.GetUserAsync(User);
            var instagram =
                _db.Instagrams.Include(instagram1 => instagram1.Proxy).FirstOrDefault(instagram1 =>
                    instagram1.Id == id && instagram1.User == user);
            if (instagram == null) return RedirectToAction("Accounts");
            var success = await _loginService.DeleteInstagram(instagram);
            return RedirectToAction("Accounts",
                new
                {
                    message = success.Succeeded && success.Value
                        ? "Аккаунт успешно удален."
                        : $"Не удалось удалить аккаунт ({success.Message})."
                });
        }
        
        [HttpGet]
        public async Task<IActionResult> EditAccount(int id)
        {
            if (id <= 0) return RedirectToAction("Accounts");
            var user = await _userManager.GetUserAsync(User);
            var instagram =
                _db.Instagrams.Include(instagram1 => instagram1.Proxy).FirstOrDefault(instagram1 =>
                    instagram1.Id == id && instagram1.User == user);
            if (instagram == null) return RedirectToAction("Accounts");
            return View(new InstagramEditViewModel
            {
                Id = instagram.Id, Country = instagram.Country, Username = instagram.Username,
                Password = instagram.Password, LikeChat = instagram.LikeChat, LikeChatType = instagram.LikeChatType
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(InstagramEditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            var instagram =
                _db.Instagrams.Include(instagram1 => instagram1.Proxy).FirstOrDefault(instagram1 =>
                    instagram1.Id == model.Id && instagram1.User == user);
            if (instagram == null) return RedirectToAction("Accounts");
            var success = await _loginService.EditInstagram(model, instagram);
            return RedirectToAction("Accounts",
                new
                {
                    message = success.Succeeded && success.Value
                        ? "Аккаунт успешно изменен."
                        : $"Не удалось изменить аккаунт ({success.Message})."
                });
        }
    }
}