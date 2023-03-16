using Soofle.Domain.Abstractions;
using Soofle.Infrastructure.DataStorage.Models.Abstractions;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting.Models;
using Soofle.Domain.Ordering;
using Soofle.Domain.Ordering.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Visitors.Sorting;

internal abstract class BaseSortingVisitor<TEntity, TVisitor, TItem> where TVisitor : ISortingVisitor<TVisitor, TItem>
    where TEntity : IAggregateModel
    where TItem : AggregateRoot
{
    public List<SortData<TEntity>> SortItems { get; } = new();
    protected abstract List<SortData<TEntity>> ConvertOrderToList(IOrderBy<TItem, TVisitor> spec);

    public void Visit(DescendingOrder<TItem, TVisitor> spec)
    {
        var x = ConvertOrderToList(spec.OrderData);
        SortItems.AddRange(x.Take(x.Count - 1));
        var last = x.Last();
        SortItems.Add(new SortData<TEntity>(last.Expr, true));
    }

    public void Visit(ThenByOrder<TItem, TVisitor> order)
    {
        var left = ConvertOrderToList(order.Left);
        var right = ConvertOrderToList(order.Right);
        SortItems.AddRange(left);
        SortItems.AddRange(right);
    }
}