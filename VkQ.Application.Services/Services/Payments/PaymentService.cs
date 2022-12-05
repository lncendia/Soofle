using VkQ.Application.Abstractions.Payments.DTOs;
using VkQ.Application.Abstractions.Payments.Exceptions;
using VkQ.Application.Abstractions.Payments.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Ordering;
using VkQ.Domain.Transactions.Entities;
using VkQ.Domain.Transactions.Exceptions;
using VkQ.Domain.Transactions.Ordering;
using VkQ.Domain.Transactions.Ordering.Visitor;
using VkQ.Domain.Transactions.Specification;

namespace VkQ.Application.Services.Services.Payments;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentCreatorService _paymentCreatorService;

    public PaymentService(IUnitOfWork unitOfWork, IPaymentCreatorService paymentCreatorService)
    {
        _unitOfWork = unitOfWork;
        _paymentCreatorService = paymentCreatorService;
    }

    /// <exception cref="ErrorCreateBillException"></exception>
    public async Task<string> CreatePaymentAsync(Guid userId, decimal amount)
    {
        var paymentData = await _paymentCreatorService.CreatePayAsync(userId, amount);
        var transaction = new Transaction(paymentData.BillId, paymentData.PayUrl, amount, userId);
        await _unitOfWork.TransactionRepository.Value.AddAsync(transaction);
        await _unitOfWork.SaveChangesAsync();
        return paymentData.PayUrl;
    }

    /// <exception cref="TransactionNotFoundException"></exception>
    /// <exception cref="BillNotPaidException"></exception>
    /// <exception cref="TransactionAlreadyAcceptedException"></exception>
    /// <exception cref="ErrorCheckBillException"></exception>
    public async Task ConfirmPaymentAsync(Guid userId, Guid transactionId)
    {
        var transaction = await _unitOfWork.TransactionRepository.Value.GetAsync(transactionId);
        if (transaction == null) throw new TransactionNotFoundException();
        if (transaction.UserId != userId) throw new TransactionNotFoundException();
        var data = await _paymentCreatorService.CheckPaymentAsync(transaction.PaymentSystemId);
        if (!data) throw new BillNotPaidException(transaction.PaymentSystemId);
        transaction.AcceptPayment();
        await _unitOfWork.TransactionRepository.Value.UpdateAsync(transaction);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<PaymentDto>> GetPaymentsAsync(Guid userId, int page)
    {
        if (page < 1) throw new ArgumentOutOfRangeException(nameof(page));
        var payments = await _unitOfWork.TransactionRepository.Value.FindAsync(
            new TransactionByUserIdSpecification(userId),
            new DescendingOrder<Transaction, ITransactionSortingVisitor>(new TransactionByCreationDateOrder()),
            page * 10, 10);
        return payments.Select(x =>
                new PaymentDto(x.PaymentSystemId, x.Amount, x.CreationDate, x.IsSuccessful, x.ConfirmationDate, x.PaymentSystemUrl))
            .ToList();
    }
}