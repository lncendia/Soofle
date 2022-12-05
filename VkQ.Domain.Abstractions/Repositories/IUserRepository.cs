using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.Ordering.Visitor;
using VkQ.Domain.Users.Specification.Visitor;

namespace VkQ.Domain.Abstractions.Repositories;

public interface IUserRepository : IRepository<User, IUserSpecificationVisitor, IUserSortingVisitor>
{
}