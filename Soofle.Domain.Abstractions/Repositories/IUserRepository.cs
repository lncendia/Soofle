using Soofle.Domain.Users.Entities;
using Soofle.Domain.Users.Ordering.Visitor;
using Soofle.Domain.Users.Specification.Visitor;

namespace Soofle.Domain.Abstractions.Repositories;

public interface IUserRepository : IRepository<User, IUserSpecificationVisitor, IUserSortingVisitor>
{
}