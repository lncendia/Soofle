using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Links.Entities;
using VkQ.Domain.Links.Ordering.Visitor;
using VkQ.Infrastructure.DataStorage.Models;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class LinkSortingVisitor : BaseSortingVisitor<LinkModel, ILinkSortingVisitor, Link>, ILinkSortingVisitor
{
    protected override List<SortData<LinkModel>> ConvertOrderToList(IOrderBy<Link, ILinkSortingVisitor> spec)
    {
        var visitor = new LinkSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}