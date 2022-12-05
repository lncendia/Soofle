using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Transactions.Entities;
using VkQ.Domain.Transactions.Ordering.Visitor;

namespace VkQ.Domain.Transactions.Ordering;

public class TransactionByCreationDateOrder : IOrderBy<Transaction, ITransactionSortingVisitor>
{
    public IEnumerable<Transaction> Order(IEnumerable<Transaction> items) => items.OrderBy(x => x.CreationDate);

    public IList<IEnumerable<Transaction>> Divide(IEnumerable<Transaction> items) =>
        Order(items).GroupBy(x => x.CreationDate.Date).Select(x => x.AsEnumerable()).ToList();

    public void Accept(ITransactionSortingVisitor visitor) => visitor.Visit(this);
}