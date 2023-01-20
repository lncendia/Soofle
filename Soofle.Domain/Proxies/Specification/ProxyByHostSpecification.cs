using Soofle.Domain.Proxies.Entities;
using Soofle.Domain.Proxies.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Proxies.Specification;

public class ProxyByHostSpecification : ISpecification<Proxy, IProxySpecificationVisitor>
{
    public ProxyByHostSpecification(string hostName)
    {
        HostName = hostName;
    }

    public string HostName { get; }
    public bool IsSatisfiedBy(Proxy item) => item.Host == HostName;

    public void Accept(IProxySpecificationVisitor visitor) => visitor.Visit(this);
}