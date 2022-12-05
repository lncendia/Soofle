using VkQ.Application.Abstractions.Payments.DTOs;
using VkQ.Application.Abstractions.Proxies.DTOs;

namespace VkQ.Application.Abstractions.Payments.ServicesInterfaces;

public interface IPaymentService
{
    public Task<string> CreatePaymentAsync(Guid userId, decimal amount);
    public Task ConfirmPaymentAsync(Guid userId, Guid transactionId);
    public Task<List<PaymentDto>> GetPaymentsAsync(Guid userId, int page);
}