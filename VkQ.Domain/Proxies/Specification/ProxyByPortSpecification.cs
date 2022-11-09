using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Proxies.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Proxies.Specification;

public class ProxyByPortSpecification : ISpecification<Proxy, IProxySpecificationVisitor>
{
    public ProxyByPortSpecification(int port)
    {
        Port = port;
    }

    public int Port { get; }
    public bool IsSatisfiedBy(Proxy item) => item.Port == Port;

    public void Accept(IProxySpecificationVisitor visitor) => visitor.Visit(this);
}