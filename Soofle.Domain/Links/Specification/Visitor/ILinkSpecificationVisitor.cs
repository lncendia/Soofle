using Soofle.Domain.Links.Entities;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Links.Specification.Visitor;

public interface ILinkSpecificationVisitor : ISpecificationVisitor<ILinkSpecificationVisitor, Link>
{
    void Visit(LinkByUserIdSpecification specification);
    void Visit(LinkByUserIdsSpecification specification);
    void Visit(AcceptedLinkSpecification specification);
}