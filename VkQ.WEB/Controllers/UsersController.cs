using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VkQ.WEB.ViewModels.Users;

namespace VkQ.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly PaymentService _paymentService;
        private readonly TimeService _timeService;
        private readonly UserService _userService;

        public UsersController(UserManager<User> userManager, ApplicationDbContext context,
            PaymentService paymentService, TimeService timeService, UserService userService)
        {
            _userManager = userManager;
            _context = context;
            _paymentService = paymentService;
            _timeService = timeService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index(UsersViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Message)) ViewData["Alert"] = model.Message;
            model.Count = _userService.GetUsersCount(model);
            if ((model.Page - 1) * 30 > model.Count) model.Page = 1;
            model.Users = _userService.GetUsers(model);
            return View(model);
        }
        

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index", new { Message = "Пользователь не найден." });
            }

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id, Email = user.Email, Username = user.UserName, Subscribe = user.EndOfSubscribe,
                Block = user.LockoutEnd?.DateTime,
                Admin = await _userManager.IsInRoleAsync(user, "admin"),
                Rates = _paymentService.GetRates()
            };
            user.Rate = _context.Rates.FirstOrDefault(rate1 => rate1.Users.Contains(user));
            if (user.Rate != null) model.RateId = user.Rate.Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            User user = _userManager.Users.Include(user1 => user1.Rate).FirstOrDefault(user1 => user1.Id == model.Id);
            if (user == null) return View(model);
            var currentUser = await _userManager.GetUserAsync(User);
            var timeZone = _timeService.GetTimeZoneInfo(currentUser);
            var result = await _userService.EditUser(model, user, timeZone);
            model.Rates = _paymentService.GetRates();
            
            if (result.Succeeded && result.Value)
                return RedirectToAction("Index", new {Message = "Пользователь успешно изменен."});
            if(!string.IsNullOrEmpty(result.Message)) ModelState.AddModelError("", result.Message);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index", new { Message = "Пользователь не найден." });
            }

            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Username = user.UserName };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            User user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                var passwordValidator =
                    HttpContext.RequestServices.GetService(
                        typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                var passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                if (passwordValidator == null || passwordHasher == null) return View(model);
                IdentityResult result =
                    await passwordValidator.ValidateAsync(_userManager, user, model.Password);
                if (result.Succeeded)
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, model.Password);
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index", new { Message = "Пароль успешно изменен." });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }

            return View(model);
        }
    }
}