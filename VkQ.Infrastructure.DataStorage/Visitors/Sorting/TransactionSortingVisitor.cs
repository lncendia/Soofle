using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Transactions.Entities;
using VkQ.Domain.Transactions.Ordering;
using VkQ.Domain.Transactions.Ordering.Visitor;
using VkQ.Infrastructure.DataStorage.Models;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class TransactionSortingVisitor :
    BaseSortingVisitor<TransactionModel, ITransactionSortingVisitor, Transaction>, ITransactionSortingVisitor
{
    protected override List<SortData<TransactionModel>> ConvertOrderToList(
        IOrderBy<Transaction, ITransactionSortingVisitor> spec)
    {
        var visitor = new TransactionSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }

    public void Visit(TransactionByCreationDateOrder sorting) =>
        SortItems.Add(new SortData<TransactionModel>(x => x.CreationDate, false));
}