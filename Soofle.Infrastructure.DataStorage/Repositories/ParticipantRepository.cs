﻿using Microsoft.EntityFrameworkCore;
using Soofle.Domain.Abstractions.Repositories;
using Soofle.Infrastructure.DataStorage.Context;
using Soofle.Infrastructure.DataStorage.Mappers.Abstractions;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting;
using Soofle.Infrastructure.DataStorage.Visitors.Specifications;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Ordering.Visitor;
using Soofle.Domain.Participants.Specification.Visitor;

namespace Soofle.Infrastructure.DataStorage.Repositories;

internal class ParticipantRepository : IParticipantRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IAggregateMapperUnit<Participant, ParticipantModel> _mapper;
    private readonly IModelMapperUnit<ParticipantModel, Participant> _modelMapper;
    private readonly ParticipantVisitor _visitor = new();
    private readonly ParticipantSortingVisitor _sortingVisitor = new();


    public ParticipantRepository(ApplicationDbContext context, IAggregateMapperUnit<Participant, ParticipantModel> mapper,
        IModelMapperUnit<ParticipantModel, Participant> modelMapper)
    {
        _context = context;
        _mapper = mapper;
        _modelMapper = modelMapper;
    }

    public async Task<Participant?> GetAsync(Guid id)
    {
        var model = await _context.Participants.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return model == null ? null : _mapper.Map(model);
    }

    public async Task AddAsync(Participant entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Notifications.AddRange(entity.DomainEvents);
        _context.Add(model);
    }

    public async Task UpdateAsync(Participant entity)
    {
        var model = await _modelMapper.MapAsync(entity);
        _context.Notifications.AddRange(entity.DomainEvents);
        _context.Update(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await _context.Participants.FirstOrDefaultAsync(x => x.Id == id);
        if (model != null) _context.Remove(model);
    }

    public async Task DeleteAsync(ISpecification<Participant, IParticipantSpecificationVisitor> specification)
    {
        var query = _context.Participants.AsQueryable();
        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        _context.RemoveRange(await query.ToListAsync());
    }

    public Task<int> CountAsync(ISpecification<Participant, IParticipantSpecificationVisitor>? specification)
    {
        var query = _context.Participants.AsQueryable();
        if (specification == null) return query.CountAsync();

        specification.Accept(_visitor);
        if (_visitor.Expr != null) query = query.Where(_visitor.Expr);
        return query.CountAsync();
    }

    public async Task<List<Participant>> FindAsync(
        ISpecification<Participant, IParticipantSpecificationVisitor>? specification = null,
        IOrderBy<Participant, IParticipantSortingVisitor>? orderBy = null, int? skip = null, int? take = null)
    {
        var query = _context.Participants.AsQueryable();
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