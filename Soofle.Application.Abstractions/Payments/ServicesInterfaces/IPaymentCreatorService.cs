using Soofle.Application.Abstractions.Payments.DTOs;

namespace Soofle.Application.Abstractions.Payments.ServicesInterfaces;

public interface IPaymentCreatorService
{
    Task<PaymentData> CreateAsync(Guid id, decimal cost);
    Task<bool> CheckAsync(string billId);
}