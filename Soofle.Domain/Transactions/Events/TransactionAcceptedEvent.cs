using Soofle.Domain.Abstractions;

namespace Soofle.Domain.Transactions.Events;

public class TransactionAcceptedEvent : IDomainEvent
{
    public TransactionAcceptedEvent(Guid userId, decimal amount)
    {
        UserId = userId;
        Amount = amount;
    }

    public Guid UserId { get; }
    public decimal Amount { get; }
}