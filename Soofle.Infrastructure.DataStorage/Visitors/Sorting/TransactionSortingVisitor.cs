using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting.Models;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Transactions.Entities;
using Soofle.Domain.Transactions.Ordering;
using Soofle.Domain.Transactions.Ordering.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Sorting;

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