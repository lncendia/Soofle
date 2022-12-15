using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Abstractions.Repositories;
using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Proxies.Ordering.Visitor;
using VkQ.Domain.Proxies.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting;
using VkQ.Infrastructure.DataStorage.Visitors.Specifications;

namespace VkQ.Infrastructure.DataStorage.Repositories;

internal class ProxyRepository : IProxyRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IAggregateMapperUnit<Proxy, ProxyModel> _mapper;
    private readonly IModelMapperUnit<ProxyModel, Proxy> _modelMapper;
    private readonly ProxyVisitor _visitor = new();
    private readonly ProxySortingVisitor _sortingVisitor = new();


    public ProxyRepository(ApplicationDbContext context, IAggregateMapperUnit<Proxy, ProxyModel> mapper,
        IModelMapperUnit<ProxyModel, Proxy> modelMapper)
    {
        _context = context;
        _mapper = mapper;
        _modelMapper = modelMapper;
    }

    public async Task<Proxy?> GetAsync(Guid id)
    {
        var model = await _context.Proxies.FirstOrDefaultAsync(x => x.Id == id);
        return model == null ? null : _mapper.Map(model);
    }

    public async Task AddAsync(Proxy entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Add(model);
    }

    public async Task UpdateAsync(Proxy entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Update(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await _context.Proxies.FirstOrDefaultAsync(x => x.Id == id);
        if (model != null) _context.Remove(model);
    }

    public async Task DeleteAsync(ISpecification<Proxy, IProxySpecificationVisitor> specification)
    {
        var query = _context.Proxies.AsQueryable();
        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        _context.RemoveRange(await query.ToListAsync());
    }

    public Task<int> CountAsync(ISpecification<Proxy, IProxySpecificationVisitor>? specification)
    {
        var query = _context.Proxies.AsQueryable();
        if (specification == null) return query.CountAsync();

        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        return query.CountAsync();
    }

    public async Task<List<Proxy>> FindAsync(ISpecification<Proxy, IProxySpecificationVisitor>? specification = null,
        IOrderBy<Proxy, IProxySortingVisitor>? orderBy = null, int? skip = null, int? take = null)
    {
        var query = _context.Proxies.AsQueryable();
        if (specification != null)
        {
            specification.Accept(_visitor);
            if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        }

        if (orderBy != null)
        {
            orderBy.Accept(_sortingVisitor);
            var firstQuery = _sortingVisitor.SortItems.First();
            var orderedQuery = firstQuery.IsDescending
                ? query.OrderByDescending(firstQuery.Expr)
                : query.OrderBy(firstQuery.Expr);

            query = _sortingVisitor.SortItems.Skip(1)
                .Aggregate(orderedQuery, (current, sort) => sort.IsDescending
                    ? current.ThenByDescending(sort.Expr)
                    : current.ThenBy(sort.Expr));
        }

        if (skip.HasValue) query = query.Skip(skip.Value);
        if (take.HasValue) query = query.Take(take.Value);

        return (await query.ToListAsync()).Select(_mapper.Map).ToList();
    }
}