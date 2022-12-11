
using VkQ.Application.Abstractions.Payments.DTOs;

namespace VkQ.Application.Abstractions.Payments.ServicesInterfaces;

public interface IPaymentManager
{
    public Task<CreatePaymentDto> CreateAsync(Guid userId, decimal amount);
    public Task ConfirmAsync(Guid userId, Guid transactionId);
}