using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Transactions.Entities;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.ModelMappers;

internal class TransactionModelMapper : IModelMapper<TransactionModel, Transaction>
{
    private readonly ApplicationDbContext _context;

    public TransactionModelMapper(ApplicationDbContext context) => _context = context;

    public async Task<TransactionModel> MapAsync(Transaction model)
    {
        var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == model.Id) ??
                          new TransactionModel { Id = model.Id };

        transaction.Amount = model.Amount;
        transaction.ConfirmationDate = model.ConfirmationDate;
        transaction.CreationDate = model.CreationDate;
        transaction.IsSuccessful = model.IsSuccessful;
        transaction.UserId = model.UserId;
        transaction.PaymentSystemId = model.PaymentSystemId;
        transaction.PaymentSystemUrl = model.PaymentSystemUrl;

        return transaction;
    }
}