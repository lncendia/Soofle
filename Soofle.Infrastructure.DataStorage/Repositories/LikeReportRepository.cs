using Microsoft.EntityFrameworkCore;
using Soofle.Domain.Abstractions.Repositories;
using Soofle.Infrastructure.DataStorage.Context;
using Soofle.Infrastructure.DataStorage.Mappers.Abstractions;
using Soofle.Infrastructure.DataStorage.Models.Reports.LikeReport;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting;
using Soofle.Infrastructure.DataStorage.Visitors.Specifications;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.ReportLogs.Enums;
using Soofle.Domain.Reposts.BaseReport.Events;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Ordering.Visitor;
using Soofle.Domain.Reposts.LikeReport.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Repositories;

internal class LikeReportRepository : ILikeReportRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IAggregateMapperUnit<LikeReport, LikeReportModel> _mapper;
    private readonly IModelMapperUnit<LikeReportModel, LikeReport> _modelMapper;
    private readonly LikeReportVisitor _visitor = new();
    private readonly LikeReportSortingVisitor _sortingVisitor = new();


    public LikeReportRepository(ApplicationDbContext context, IAggregateMapperUnit<LikeReport, LikeReportModel> mapper,
        IModelMapperUnit<LikeReportModel, LikeReport> modelMapper)
    {
        _context = context;
        _mapper = mapper;
        _modelMapper = modelMapper;
    }

    public async Task<LikeReport?> GetAsync(Guid id)
    {
        var model = await _context.LikeReports.FirstOrDefaultAsync(x => x.Id == id);
        if (model == null) return null;
        await LoadCollectionsAsync(model);
        return _mapper.Map(model);
    }

    public async Task AddAsync(LikeReport entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Notifications.AddRange(entity.DomainEvents);
        _context.Add(model);
    }

    public async Task UpdateAsync(LikeReport entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Notifications.AddRange(entity.DomainEvents);
        _context.Update(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await _context.LikeReports.FirstOrDefaultAsync(x => x.Id == id);
        if (model != null)
        {
            _context.Remove(model);
            _context.Notifications.Add(new ReportDeletedEvent(id));
        }
    }

    public Task<int> CountAsync(ISpecification<LikeReport, ILikeReportSpecificationVisitor>? specification)
    {
        var query = _context.LikeReports.AsQueryable();
        if (specification == null) return query.CountAsync();

        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        return query.CountAsync();
    }

    public async Task<List<LikeReport>> FindAsync(
        ISpecification<LikeReport, ILikeReportSpecificationVisitor>? specification = null,
        IOrderBy<LikeReport, ILikeReportSortingVisitor>? orderBy = null, int? skip = null, int? take = null)
    {
        var query = _context.LikeReports.Include(x => x.Publications).Include(x => x.LinkedUsers)
            .Include(x => x.ReportElementsList).AsQueryable();
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

        var reports = await query.AsNoTracking().ToListAsync();
        foreach (var report in reports) await LoadCollectionsAsync(report);

        return reports.Select(_mapper.Map).ToList();
    }

    private async Task LoadCollectionsAsync(LikeReportModel model)
    {
        await _context.Entry(model).Collection(x => x.Publications).LoadAsync();
        await _context.Entry(model).Collection(x => x.ReportElementsList).LoadAsync();
        await _context.Entry(model).Collection(x => x.LinkedUsers).LoadAsync();
    }
}