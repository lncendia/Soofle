using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Users.Specification.Visitor;

public interface IUserSpecificationVisitor : ISpecificationVisitor<IUserSpecificationVisitor, User>
{
    void Visit(UserByEmailSpecification specification);
    void Visit(UserByIdSpecification specification);
}