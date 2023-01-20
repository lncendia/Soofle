using Microsoft.EntityFrameworkCore;
using Soofle.Domain.Abstractions.Repositories;
using Soofle.Infrastructure.DataStorage.Context;
using Soofle.Infrastructure.DataStorage.Mappers.Abstractions;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting;
using Soofle.Infrastructure.DataStorage.Visitors.Specifications;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Ordering.Visitor;
using Soofle.Domain.ReportLogs.Specification.Visitor;

namespace Soofle.Infrastructure.DataStorage.Repositories;

internal class ReportLogRepository : IReportLogRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IAggregateMapperUnit<ReportLog, ReportLogModel> _mapper;
    private readonly IModelMapperUnit<ReportLogModel, ReportLog> _modelMapper;
    private readonly ReportLogVisitor _visitor = new();
    private readonly ReportLogSortingVisitor _sortingVisitor = new();


    public ReportLogRepository(ApplicationDbContext context, IAggregateMapperUnit<ReportLog, ReportLogModel> mapper,
        IModelMapperUnit<ReportLogModel, ReportLog> modelMapper)
    {
        _context = context;
        _mapper = mapper;
        _modelMapper = modelMapper;
    }

    public async Task<ReportLog?> GetAsync(Guid id)
    {
        var model = await _context.ReportLogs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return model == null ? null : _mapper.Map(model);
    }

    public async Task AddAsync(ReportLog entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Notifications.AddRange(entity.DomainEvents);
        _context.Add(model);
    }

    public async Task UpdateAsync(ReportLog entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Notifications.AddRange(entity.DomainEvents);
        _context.Update(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await _context.ReportLogs.FirstOrDefaultAsync(x => x.Id == id);
        if (model != null) _context.Remove(model);
    }

    public async Task DeleteAsync(ISpecification<ReportLog, IReportLogSpecificationVisitor> specification)
    {
        var query = _context.ReportLogs.AsQueryable();
        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        _context.RemoveRange(await query.ToListAsync());
    }

    public Task<int> CountAsync(ISpecification<ReportLog, IReportLogSpecificationVisitor>? specification)
    {
        var query = _context.ReportLogs.AsQueryable();
        if (specification == null) return query.CountAsync();

        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        return query.CountAsync();
    }

    public async Task<List<ReportLog>> FindAsync(
        ISpecification<ReportLog, IReportLogSpecificationVisitor>? specification = null,
        IOrderBy<ReportLog, IReportLogSortingVisitor>? orderBy = null, int? skip = null, int? take = null)
    {
        var query = _context.ReportLogs.AsQueryable();
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

        return (await query.AsNoTracking().ToListAsync()).Select(_mapper.Map).ToList();
    }
}