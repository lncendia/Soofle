using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting.Models;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Proxies.Entities;
using Soofle.Domain.Proxies.Ordering;
using Soofle.Domain.Proxies.Ordering.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Sorting;

internal class ProxySortingVisitor : BaseSortingVisitor<ProxyModel, IProxySortingVisitor, Proxy>, IProxySortingVisitor
{
    protected override List<SortData<ProxyModel>> ConvertOrderToList(IOrderBy<Proxy, IProxySortingVisitor> spec)
    {
        var visitor = new ProxySortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }

    public void Visit(ProxyByRandomOrder order) =>
        SortItems.Add(new SortData<ProxyModel>(x => Guid.NewGuid(), false));
}