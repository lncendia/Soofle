using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Transactions.Entities;

namespace Soofle.Domain.Transactions.Ordering.Visitor;

public interface ITransactionSortingVisitor : ISortingVisitor<ITransactionSortingVisitor, Transaction>
{
    void Visit(TransactionByCreationDateOrder sorting);
}