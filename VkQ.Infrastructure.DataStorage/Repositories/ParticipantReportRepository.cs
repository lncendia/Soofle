using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Abstractions.Repositories;
using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Ordering.Visitor;
using VkQ.Domain.Reposts.ParticipantReport.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting;
using VkQ.Infrastructure.DataStorage.Visitors.Specifications;

namespace VkQ.Infrastructure.DataStorage.Repositories;

internal class ParticipantReportRepository : IParticipantReportRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IAggregateMapper<ParticipantReport, ParticipantReportModel> _mapper;
    private readonly IModelMapper<ParticipantReportModel, ParticipantReport> _modelMapper;
    private readonly ParticipantReportVisitor _visitor = new();
    private readonly ParticipantReportSortingVisitor _sortingVisitor = new();


    public ParticipantReportRepository(ApplicationDbContext context,
        IAggregateMapper<ParticipantReport, ParticipantReportModel> mapper,
        IModelMapper<ParticipantReportModel, ParticipantReport> modelMapper)
    {
        _context = context;
        _mapper = mapper;
        _modelMapper = modelMapper;
    }

    public async Task<ParticipantReport?> GetAsync(Guid id)
    {
        var model = await _context.ParticipantReports
            .Include(x => x.ReportElementsList.Cast<ParticipantReportElementModel>())
            .FirstOrDefaultAsync(x => x.Id == id);
        return model == null ? null : _mapper.Map(model);
    }

    public async Task AddAsync(ParticipantReport entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Add(model);
    }

    public async Task UpdateAsync(ParticipantReport entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Update(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await _context.ParticipantReports.FirstOrDefaultAsync(x => x.Id == id);
        if (model != null) _context.Remove(model);
    }

    public async Task DeleteAsync(
        ISpecification<ParticipantReport, IParticipantReportSpecificationVisitor> specification)
    {
        var query = _context.ParticipantReports.AsQueryable();
        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        _context.RemoveRange(await query.ToListAsync());
    }

    public Task<int> CountAsync(
        ISpecification<ParticipantReport, IParticipantReportSpecificationVisitor>? specification)
    {
        var query = _context.ParticipantReports.AsQueryable();
        if (specification == null) return query.CountAsync();

        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        return query.CountAsync();
    }

    public async Task<IList<ParticipantReport>> FindAsync(
        ISpecification<ParticipantReport, IParticipantReportSpecificationVisitor>? specification = null,
        IOrderBy<ParticipantReport, IParticipantReportSortingVisitor>? orderBy = null, int? skip = null,
        int? take = null)
    {
        var query = _context.ParticipantReports.Include(x => x.ReportElementsList.Cast<ParticipantReportElementModel>())
            .AsQueryable();
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