using VkQ.Application.Abstractions.Payments.DTOs;

namespace VkQ.Application.Abstractions.Payments.ServicesInterfaces;

public interface IPaymentService
{
    public Task<PaymentDto> CreatePaymentAsync(Guid userId, decimal amount);
    public Task ConfirmPaymentAsync(Guid userId, Guid transactionId);
    public Task<List<PaymentDto>> GetPaymentsAsync(Guid userId, int page);
}