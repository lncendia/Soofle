using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Proxies.Ordering.Visitor;
using VkQ.Domain.Proxies.Specification.Visitor;

namespace VkQ.Domain.Abstractions.Repositories;

public interface IProxyRepository : IRepository<Proxy, IProxySpecificationVisitor, IProxySortingVisitor>
{
}