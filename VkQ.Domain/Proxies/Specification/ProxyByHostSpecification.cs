using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Proxies.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Proxies.Specification;

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