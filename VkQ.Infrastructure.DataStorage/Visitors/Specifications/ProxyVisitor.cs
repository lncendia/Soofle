using System.Linq.Expressions;
using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Proxies.Specification;
using VkQ.Domain.Proxies.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class ProxyVisitor : BaseVisitor<ProxyModel, IProxySpecificationVisitor, Proxy>, IProxySpecificationVisitor
{
    protected override Expression<Func<ProxyModel, bool>> ConvertSpecToExpression(
        ISpecification<Proxy, IProxySpecificationVisitor> spec)
    {
        var visitor = new ProxyVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }

    public void Visit(ProxyByHostSpecification specification) => Expr = x => x.Host == specification.HostName;

    public void Visit(ProxyByPortSpecification specification) => Expr = x => x.Port == specification.Port;
}