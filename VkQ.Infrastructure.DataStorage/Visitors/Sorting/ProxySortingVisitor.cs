using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Proxies.Ordering;
using VkQ.Domain.Proxies.Ordering.Visitor;
using VkQ.Infrastructure.DataStorage.Models;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class ProxySortingVisitor : BaseSortingVisitor<ProxyModel, IProxySortingVisitor, Proxy>, IProxySortingVisitor
{
    protected override List<SortData<ProxyModel>> ConvertOrderToList(IOrderBy<Proxy, IProxySortingVisitor> spec)
    {
        var visitor = new ProxySortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }

    public void Visit(ProxyByRandomOrder order) => SortItems.Add(new SortData<ProxyModel>(x => Guid.NewGuid(), false));
}