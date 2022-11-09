using System.Linq.Expressions;

namespace VkQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

public class SortData<TEntity>
{
    public SortData(Expression<Func<TEntity, dynamic>> expr, bool isDescending)
    {
        Expr = expr;
        IsDescending = isDescending;
    }

    public Expression<Func<TEntity, dynamic>> Expr { get; }
    public bool IsDescending { get; }
}