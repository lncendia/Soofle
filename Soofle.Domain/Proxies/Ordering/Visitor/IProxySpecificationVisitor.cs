using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Proxies.Entities;

namespace Soofle.Domain.Proxies.Ordering.Visitor;

public interface IProxySortingVisitor : ISortingVisitor<IProxySortingVisitor, Proxy>
{
    void Visit(ProxyByRandomOrder order);
}