using System.Linq.Expressions;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Users.Entities;
using Soofle.Domain.Users.Specification;
using Soofle.Domain.Users.Specification.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Specifications;

internal class UserVisitor : BaseVisitor<UserModel, IUserSpecificationVisitor, User>, IUserSpecificationVisitor
{
    protected override Expression<Func<UserModel, bool>> ConvertSpecToExpression(
        ISpecification<User, IUserSpecificationVisitor> spec)
    {
        var visitor = new UserVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }

    public void Visit(UserByEmailSpecification specification) => Expr = x => x.Email == specification.Email;

    public void Visit(UserByNameSpecification specification) =>
        Expr = x => x.Name.ToUpper().Contains(specification.Name.ToUpper());

    public void Visit(UserByIdSpecification specification) => Expr = x => x.Id == specification.Id;

    public void Visit(UserByIdsSpecification specification) => Expr = x => specification.Ids.Contains(x.Id);
}