using VkQ.Application.Abstractions.DTO.Payments;

namespace VkQ.Application.Abstractions.Interfaces.Payments;

public interface IPaymentCreatorService
{
    Task<PaymentData> CreatePayAsync(long id, decimal cost);
    Task<(decimal, DateTime)> GetPaymentData(string billId);
}