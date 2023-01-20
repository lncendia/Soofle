using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Users.Entities;

namespace Soofle.Domain.Users.Ordering.Visitor;

public interface IUserSortingVisitor : ISortingVisitor<IUserSortingVisitor, User>
{
    void Visit(UserByNameOrder order);
}