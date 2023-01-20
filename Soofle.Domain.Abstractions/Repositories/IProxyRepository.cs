using Soofle.Domain.Proxies.Entities;
using Soofle.Domain.Proxies.Ordering.Visitor;
using Soofle.Domain.Proxies.Specification.Visitor;

namespace Soofle.Domain.Abstractions.Repositories;

public interface IProxyRepository : IRepository<Proxy, IProxySpecificationVisitor, IProxySortingVisitor>
{
}