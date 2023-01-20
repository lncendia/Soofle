using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Users.Entities;
using Soofle.Domain.Users.Specification.Visitor;

namespace Soofle.Domain.Users.Specification;

public class UserByNameSpecification : ISpecification<User, IUserSpecificationVisitor>
{
    public string Name { get; }
    public UserByNameSpecification(string name) => Name = name;

    public bool IsSatisfiedBy(User item) => item.Name.ToUpper().Contains(Name.ToUpper());

    public void Accept(IUserSpecificationVisitor visitor) => visitor.Visit(this);
}