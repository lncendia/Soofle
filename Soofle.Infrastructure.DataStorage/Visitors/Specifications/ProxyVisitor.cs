using System.Linq.Expressions;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.Proxies.Entities;
using Soofle.Domain.Proxies.Specification;
using Soofle.Domain.Proxies.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Visitors.Specifications;

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