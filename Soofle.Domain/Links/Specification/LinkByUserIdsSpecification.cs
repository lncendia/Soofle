using Soofle.Domain.Links.Entities;
using Soofle.Domain.Links.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Links.Specification;

public class LinkByUserIdsSpecification : ISpecification<Link, ILinkSpecificationVisitor>
{
    public List<Guid> Ids { get; }

    public LinkByUserIdsSpecification(Guid id1, Guid id2)
    {
        Ids = new List<Guid> { id1, id2 };
    }

    public bool IsSatisfiedBy(Link item) => Ids.Contains(item.User1Id) && Ids.Contains(item.User2Id);

    public void Accept(ILinkSpecificationVisitor visitor) => visitor.Visit(this);
}