using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VkQ.WEB.ViewModels.Payment;

namespace VkQ.WEB.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly PaymentService _paymentService;
        private readonly ApplicationDbContext _dbContext;

        public PaymentController(UserManager<User> userManager, PaymentService paymentService,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _paymentService = paymentService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> MyPayments(string message, int page = 1)
        {
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            var user = await _userManager.GetUserAsync(User);
            user.Rate = _dbContext.Rates.FirstOrDefault(rate1 => rate1.Users.Contains(user));
            var model = new MyPaymentsViewModel()
            {
                User = user,
                Count = _paymentService.GetPaymentsCount(user),
                Rates = _paymentService.GetRates()
            };
            if ((page - 1) * 30 > model.Count) page = 1;
            model.Payments = _paymentService.GetPayments(user, page);
            model.Page = page;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePayment(int id)
        {
            if (id <= 0) return RedirectToAction("MyPayments", new { message = "Вы не выбрали тариф." });
            var rate = _dbContext.Rates.FirstOrDefault(rate1 => rate1.Id == id);
            if (rate == null) return RedirectToAction("MyPayments", new { message = "Тариф не найден." });
            var user = await _userManager.GetUserAsync(User);
            var payment = _paymentService.CreateBill(user, rate);
            if (payment != null) return Redirect(payment.PayUrl);
            return RedirectToAction("MyPayments", new { message = "Произошла ошибка при создании счёта." });
        }

        [HttpGet]
        public async Task<IActionResult> CheckPayment(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            var payment = _dbContext.Payments.Include(payment1 => payment1.User.Rate).Include(payment1 => payment1.Rate)
                .FirstOrDefault(payment1 =>
                    payment1.Id == id && payment1.User == user && !payment1.Success);
            return payment == null
                ? RedirectToAction("MyPayments", new { message = "Платеж не найден." })
                : RedirectToAction("MyPayments",
                    _paymentService.CheckPayment(payment)
                        ? new { message = "Платеж зачислен." }
                        : new { message = "Счёт не оплачен." });
        }
    }
}