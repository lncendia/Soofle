using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Proxies.Entities;

namespace VkQ.Domain.Proxies.Ordering.Visitor;

public interface IProxySortingVisitor : ISortingVisitor<IProxySortingVisitor, Proxy>
{
    void Visit(ProxyByRandomOrder order);
}