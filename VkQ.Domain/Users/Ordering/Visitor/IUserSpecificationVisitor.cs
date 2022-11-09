using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Users.Ordering.Visitor;

public interface IUserSortingVisitor : ISortingVisitor<IUserSortingVisitor, User>
{
}