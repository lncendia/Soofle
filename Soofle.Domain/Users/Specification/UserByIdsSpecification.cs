using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Users.Entities;
using Soofle.Domain.Users.Specification.Visitor;

namespace Soofle.Domain.Users.Specification;

public class UserByIdsSpecification : ISpecification<User, IUserSpecificationVisitor>
{
    public List<Guid> Ids { get; }
    public UserByIdsSpecification(List<Guid> ids) => Ids = ids;

    public bool IsSatisfiedBy(User item) => Ids.Contains(item.Id);

    public void Accept(IUserSpecificationVisitor visitor) => visitor.Visit(this);
}