using System.Net;
using Newtonsoft.Json;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;
using RestSharp;
using Soofle.Application.Abstractions.Payments.DTOs;
using Soofle.Application.Abstractions.Payments.Exceptions;
using Soofle.Application.Abstractions.Payments.ServicesInterfaces;
using Soofle.Application.Abstractions.Proxies.DTOs;

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