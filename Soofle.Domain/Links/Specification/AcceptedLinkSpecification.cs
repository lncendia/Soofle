using Soofle.Domain.Links.Entities;
using Soofle.Domain.Links.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Links.Specification;

public class AcceptedLinkSpecification : ISpecification<Link, ILinkSpecificationVisitor>
{
    public bool IsSatisfiedBy(Link item) => item.IsConfirmed;

    public void Accept(ILinkSpecificationVisitor visitor) => visitor.Visit(this);
}