using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Abstractions.Repositories;
using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.LikeReport.Ordering.Visitor;
using VkQ.Domain.Reposts.LikeReport.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Models.Reports.LikeReport;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting;
using VkQ.Infrastructure.DataStorage.Visitors.Specifications;

namespace VkQ.Infrastructure.DataStorage.Repositories;

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
        var model = await _context.LikeReports.Include(x => x.Publications).Include(x => x.LinkedUsers)
            .Include(x => x.ReportElementsList.Cast<LikeReportElementModel>()).ThenInclude(x => x.Likes)
            .FirstOrDefaultAsync(x => x.Id == id);
        return model == null ? null : _mapper.Map(model);
    }

    public async Task AddAsync(LikeReport entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Add(model);
    }

    public async Task UpdateAsync(LikeReport entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Update(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await _context.LikeReports.FirstOrDefaultAsync(x => x.Id == id);
        if (model != null) _context.Remove(model);
    }

    public async Task DeleteAsync(ISpecification<LikeReport, ILikeReportSpecificationVisitor> specification)
    {
        var query = _context.LikeReports.AsQueryable();
        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        _context.RemoveRange(await query.ToListAsync());
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
            .Include(x => x.ReportElementsList.Cast<LikeReportElementModel>()).ThenInclude(x => x.Likes).AsQueryable();
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