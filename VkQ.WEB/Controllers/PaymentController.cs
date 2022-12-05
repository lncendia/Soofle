using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Payments.ServicesInterfaces;

namespace VkQ.WEB.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> MyPayments(int page = 1)
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
            var payments = await _paymentService.GetPaymentsAsync(id, page);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePayment(decimal amount)
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
            try
            {
                var url = await _paymentService.CreatePaymentAsync(id, amount);
                return Redirect(url);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckPayment(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
            try
            {
                
                await _paymentService.ConfirmPaymentAsync(userId, id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}