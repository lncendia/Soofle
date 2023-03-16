using Soofle.Application.Abstractions.Payments.DTOs;
using Soofle.Application.Abstractions.Payments.ServicesInterfaces;

namespace Soofle.Infrastructure.PaymentSystem.Services;

public class TestPaymentService : IPaymentCreatorService
{
    public Task<PaymentData> CreateAsync(Guid id, decimal cost)
    {
        return Task.FromResult(new PaymentData("f", "f"));
    }

    public Task<bool> CheckAsync(string billId)
    {
        return Task.FromResult(true);
    }
}