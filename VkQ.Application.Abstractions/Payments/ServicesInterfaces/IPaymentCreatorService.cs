using VkQ.Application.Abstractions.Payments.DTOs;
using VkQ.Application.Abstractions.Proxies.DTOs;

namespace VkQ.Application.Abstractions.Payments.ServicesInterfaces;

public interface IPaymentCreatorService
{
    Task<PaymentData> CreatePayAsync(Guid id, decimal cost);
    Task<bool> CheckPaymentAsync(string billId);
}