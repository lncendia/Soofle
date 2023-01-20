
using Soofle.Application.Abstractions.Payments.DTOs;

namespace Soofle.Application.Abstractions.Payments.ServicesInterfaces;

public interface IPaymentManager
{
    public Task<CreatePaymentDto> CreateAsync(Guid userId, decimal amount);
    public Task ConfirmAsync(Guid userId, Guid transactionId);
}