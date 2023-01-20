using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Users.Entities;
using Soofle.Domain.Users.Specification.Visitor;

namespace Soofle.Domain.Users.Specification;

public class UserByEmailSpecification : ISpecification<User, IUserSpecificationVisitor>
{
    public string Email { get; }
    public UserByEmailSpecification(string email) => Email = email;

    public bool IsSatisfiedBy(User item) => item.Email == Email;

    public void Accept(IUserSpecificationVisitor visitor) => visitor.Visit(this);
}