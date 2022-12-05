using System.Linq.Expressions;
using VkQ.Domain.Links.Entities;
using VkQ.Domain.Links.Specification;
using VkQ.Domain.Links.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class LinkVisitor : BaseVisitor<LinkModel, ILinkSpecificationVisitor, Link>,
    ILinkSpecificationVisitor
{
    protected override Expression<Func<LinkModel, bool>> ConvertSpecToExpression(
        ISpecification<Link, ILinkSpecificationVisitor> spec)
    {
        var visitor = new LinkVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }

    public void Visit(LinkByUserIdSpecification specification) =>
        Expr = x => x.User1Id == specification.Id || x.User2Id == specification.Id;

    public void Visit(LinkByUserIdsSpecification specification) => Expr = x =>
        specification.Ids.Contains(x.User1Id) && specification.Ids.Contains(x.User2Id);

    public void Visit(AcceptedLinkSpecification specification) => Expr = x => x.IsAccepted;
}