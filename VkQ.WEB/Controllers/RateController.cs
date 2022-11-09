using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.WEB.ViewModels.Rate;

namespace VkQ.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class RateController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly PaymentService _paymentService;

        public RateController(ApplicationDbContext db, PaymentService paymentService)
        {
            _db = db;
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            return View(new CreateRateViewModel()
                { Rates = _db.Rates.ToList() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateRateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Rates = _db.Rates.ToList();
                return View(model);
            }

            Rate rate = new Rate
            {
                Amount = model.Amount,
                CountPosts = model.CountPosts,
                Duration = model.Duration
            };
            _db.Add(rate);
            await _db.SaveChangesAsync();
            return View(new CreateRateViewModel { Rates = _db.Rates.ToList() });
        }

        public IActionResult DeleteRate(int id)
        {
            var rate = _db.Rates.Include(rate1 => rate1.Payments).Include(rate => rate.Users)
                .FirstOrDefault(rate1 => rate1.Id == id);
            if (rate == null)
                return RedirectToAction("Index",
                    new
                    {
                        message = "Тариф не найден."
                    });
            return _paymentService.DeleteRate(rate)
                ? RedirectToAction("Index",
                    new
                    {
                        message = "Тариф успешно удален."
                    })
                : RedirectToAction("Index",
                    new
                    {
                        message = "Не удалось удалить тариф."
                    });
        }
    }
}