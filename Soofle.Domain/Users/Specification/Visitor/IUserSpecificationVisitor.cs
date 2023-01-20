using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Users.Entities;

namespace Soofle.Domain.Users.Specification.Visitor;

public interface IUserSpecificationVisitor : ISpecificationVisitor<IUserSpecificationVisitor, User>
{
    void Visit(UserByEmailSpecification specification);
    void Visit(UserByNameSpecification specification);
    void Visit(UserByIdSpecification specification);
    void Visit(UserByIdsSpecification specification);
}