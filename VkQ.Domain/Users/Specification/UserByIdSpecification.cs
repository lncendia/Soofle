using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.Specification.Visitor;

namespace VkQ.Domain.Users.Specification;

public class UserByIdSpecification : ISpecification<User, IUserSpecificationVisitor>
{
    public Guid Id { get; }
    public UserByIdSpecification(Guid id) => Id = id;

    public bool IsSatisfiedBy(User item) => item.Id == Id;

    public void Accept(IUserSpecificationVisitor visitor) => visitor.Visit(this);
}