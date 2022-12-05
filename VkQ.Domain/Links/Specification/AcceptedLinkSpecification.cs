using VkQ.Domain.Links.Entities;
using VkQ.Domain.Links.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Links.Specification;

public class AcceptedLinkSpecification : ISpecification<Link, ILinkSpecificationVisitor>
{
    public bool IsSatisfiedBy(Link item) => item.IsConfirmed;

    public void Accept(ILinkSpecificationVisitor visitor) => visitor.Visit(this);
}