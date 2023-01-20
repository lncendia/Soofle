using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting.Models;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Users.Entities;
using Soofle.Domain.Users.Ordering;
using Soofle.Domain.Users.Ordering.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Sorting;

internal class UserSortingVisitor : BaseSortingVisitor<UserModel, IUserSortingVisitor, User>, IUserSortingVisitor
{
    protected override List<SortData<UserModel>> ConvertOrderToList(IOrderBy<User, IUserSortingVisitor> spec)
    {
        var visitor = new UserSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }

    public void Visit(UserByNameOrder order) => SortItems.Add(new SortData<UserModel>(x => x.Name, false));
}