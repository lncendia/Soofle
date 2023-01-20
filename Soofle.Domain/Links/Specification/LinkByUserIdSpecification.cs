using Soofle.Domain.Links.Entities;
using Soofle.Domain.Links.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Links.Specification;

public class LinkByUserIdSpecification : ISpecification<Link, ILinkSpecificationVisitor>
{
    public Guid Id { get; }
    public LinkByUserIdSpecification(Guid id) => Id = id;

    public bool IsSatisfiedBy(Link item) => item.User1Id == Id || item.User2Id == Id;

    public void Accept(ILinkSpecificationVisitor visitor) => visitor.Visit(this);
}