using Soofle.Domain.Proxies.Entities;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Proxies.Specification.Visitor;

public interface IProxySpecificationVisitor : ISpecificationVisitor<IProxySpecificationVisitor, Proxy>
{
    void Visit(ProxyByHostSpecification specification);
    void Visit(ProxyByPortSpecification specification);
}