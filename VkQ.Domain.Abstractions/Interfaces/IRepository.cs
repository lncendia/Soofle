using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Abstractions.Interfaces;

public interface IRepository<T, out TX, out TM> where T : class
    where TX : ISpecificationVisitor<TX, T>
    where TM : ISortingVisitor<TM, T>
{
    Task<T?> GetAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);


    Task DeleteAsync(ISpecification<T, TX>? specification);
    Task<int> CountAsync(ISpecification<T, TX>? specification);
    Task<bool> AnyAsync(ISpecification<T, TX>? specification);

    Task<IList<T>> FindAsync(ISpecification<T, TX>? specification, IOrderBy<T, TM>? orderBy = null, int? skip = null,
        int? take = null);
}