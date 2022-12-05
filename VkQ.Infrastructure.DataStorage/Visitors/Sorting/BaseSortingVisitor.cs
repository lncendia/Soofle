using VkQ.Domain;
using VkQ.Domain.Abstractions;
using VkQ.Domain.Ordering;
using VkQ.Domain.Ordering.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Sorting;

internal abstract class BaseSortingVisitor<TEntity, TVisitor, TItem> where TVisitor : ISortingVisitor<TVisitor, TItem>
    where TEntity : IModel
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