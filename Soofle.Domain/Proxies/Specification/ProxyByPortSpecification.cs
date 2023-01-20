using Soofle.Domain.Proxies.Entities;
using Soofle.Domain.Proxies.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Proxies.Specification;

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