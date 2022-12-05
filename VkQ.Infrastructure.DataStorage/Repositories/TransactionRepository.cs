using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Abstractions.Repositories;
using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Transactions.Entities;
using VkQ.Domain.Transactions.Ordering.Visitor;
using VkQ.Domain.Transactions.Specification.Visitor;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting;
using VkQ.Infrastructure.DataStorage.Visitors.Specifications;

namespace VkQ.Infrastructure.DataStorage.Repositories;

internal class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IAggregateMapper<Transaction, TransactionModel> _mapper;
    private readonly IModelMapper<TransactionModel, Transaction> _modelMapper;
    private readonly TransactionVisitor _visitor = new();
    private readonly TransactionSortingVisitor _sortingVisitor = new();


    public TransactionRepository(ApplicationDbContext context, IAggregateMapper<Transaction, TransactionModel> mapper,
        IModelMapper<TransactionModel, Transaction> modelMapper)
    {
        _context = context;
        _mapper = mapper;
        _modelMapper = modelMapper;
    }

    public async Task<Transaction?> GetAsync(Guid id)
    {
        var model = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == id);
        return model == null ? null : _mapper.Map(model);
    }

    public async Task AddAsync(Transaction entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Add(model);
    }

    public async Task UpdateAsync(Transaction entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Update(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == id);
        if (model != null) _context.Remove(model);
    }

    public async Task DeleteAsync(ISpecification<Transaction, ITransactionSpecificationVisitor> specification)
    {
        var query = _context.Transactions.AsQueryable();
        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        _context.RemoveRange(await query.ToListAsync());
    }

    public Task<int> CountAsync(ISpecification<Transaction, ITransactionSpecificationVisitor>? specification)
    {
        var query = _context.Transactions.AsQueryable();
        if (specification == null) return query.CountAsync();

        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        return query.CountAsync();
    }

    public async Task<IList<Transaction>> FindAsync(
        ISpecification<Transaction, ITransactionSpecificationVisitor>? specification = null,
        IOrderBy<Transaction, ITransactionSortingVisitor>? orderBy = null, int? skip = null, int? take = null)
    {
        var query = _context.Transactions.AsQueryable();
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