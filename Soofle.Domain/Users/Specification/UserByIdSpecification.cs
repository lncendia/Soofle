using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Users.Entities;
using Soofle.Domain.Users.Specification.Visitor;

namespace Soofle.Domain.Users.Specification;

public class UserByIdSpecification : ISpecification<User, IUserSpecificationVisitor>
{
    public Guid Id { get; }
    public UserByIdSpecification(Guid id) => Id = id;

    public bool IsSatisfiedBy(User item) => Id == item.Id;

    public void Accept(IUserSpecificationVisitor visitor) => visitor.Visit(this);
}