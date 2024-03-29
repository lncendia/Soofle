﻿using System.Reflection;
using Soofle.Infrastructure.DataStorage.Mappers.Abstractions;
using Soofle.Infrastructure.DataStorage.Mappers.StaticMethods;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.Transactions.Entities;

namespace Soofle.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class TransactionMapper : IAggregateMapperUnit<Transaction, TransactionModel>
{
    private static readonly Type Type = typeof(Transaction);

    private static readonly FieldInfo ConfirmationDate =
        Type.GetField("<ConfirmationDate>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo IsSuccessful =
        Type.GetField("<IsSuccessful>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public Transaction Map(TransactionModel model)
    {
        var transaction = new Transaction(model.PaymentSystemId, model.PaymentSystemUrl, model.Amount, model.UserId);
        IdFields.AggregateId.SetValue(transaction, model.Id);
        if (!model.IsSuccessful) return transaction;
        
        ConfirmationDate.SetValue(transaction, model.ConfirmationDate);
        IsSuccessful.SetValue(transaction, true);
        return transaction;
    }
}