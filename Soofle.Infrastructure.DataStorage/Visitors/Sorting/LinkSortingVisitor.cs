using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting.Models;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Links.Entities;
using Soofle.Domain.Links.Ordering.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Sorting;

internal class LinkSortingVisitor : BaseSortingVisitor<LinkModel, ILinkSortingVisitor, Link>, ILinkSortingVisitor
{
    protected override List<SortData<LinkModel>> ConvertOrderToList(IOrderBy<Link, ILinkSortingVisitor> spec)
    {
        var visitor = new LinkSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}