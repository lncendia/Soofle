using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Proxies.Specification.Visitor;

public interface IProxySpecificationVisitor : ISpecificationVisitor<IProxySpecificationVisitor, Proxy>
{
    void Visit(ProxyByHostSpecification specification);
    void Visit(ProxyByPortSpecification specification);
}