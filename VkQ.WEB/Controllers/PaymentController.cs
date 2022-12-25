using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Payments.Exceptions;
using VkQ.Application.Abstractions.Payments.ServicesInterfaces;
using VkQ.Domain.Transactions.Exceptions;
using VkQ.WEB.ViewModels.Payments;

namespace VkQ.WEB.Controllers;

[Authorize]
public class PaymentController : Controller
{
    private readonly IPaymentManager _paymentService;
    public PaymentController(IPaymentManager paymentService) => _paymentService = paymentService;


    [HttpPost]
    public async Task<IActionResult> CreatePayment(CreatePaymentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).First();
            return BadRequest(firstError.ErrorMessage);
        }

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var payment = await _paymentService.CreateAsync(userId, model.Amount);
            return Json(new PaymentViewModel(payment.Id, payment.PaymentSystemId, payment.Amount, payment.CreationDate,
                payment.PayUrl));
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                TransactionNotFoundException => "Счёт не найден",
                BillNotPaidException => "Счёт не оплачен",
                TransactionAlreadyAcceptedException => "Счёт уже подтверждён",
                ErrorCheckBillException => "Ошибка при отправке запроса на проверку оплаты",
                _ => "Произошла ошибка при проверке оплаты"
            };
            return BadRequest(text);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CheckPayment(Guid? id)
    {
        if (!id.HasValue) return BadRequest("Id is null");
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            await _paymentService.ConfirmAsync(userId, id.Value);
            return Ok();
        }
        catch (Exception ex)
        {
            var text = ex switch
            {
                TransactionNotFoundException => "Счёт не найден",
                BillNotPaidException => "Счёт не оплачен",
                TransactionAlreadyAcceptedException => "Счёт уже подтверждён",
                ErrorCheckBillException => "Ошибка при отправке запроса на проверку оплаты",
                _ => "Произошла ошибка при проверке оплаты"
            };
            return BadRequest(text);
        }
    }
}