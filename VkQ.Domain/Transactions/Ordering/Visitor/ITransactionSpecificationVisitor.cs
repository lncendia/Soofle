using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Transactions.Entities;

namespace VkQ.Domain.Transactions.Ordering.Visitor;

public interface ITransactionSortingVisitor : ISortingVisitor<ITransactionSortingVisitor, Transaction>
{
    void Visit(TransactionByCreationDateOrder sorting);
}