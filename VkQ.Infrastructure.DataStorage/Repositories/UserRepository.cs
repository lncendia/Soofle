using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Abstractions.Repositories;
using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.Ordering.Visitor;
using VkQ.Domain.Users.Specification.Visitor;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Factories.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;
using VkQ.Infrastructure.DataStorage.Visitors.Specifications;

namespace VkQ.Infrastructure.DataStorage.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IAggregateFactory<User, UserModel> _factory;
    private readonly IModelFactory<UserModel, User> _modelFactory;

    public UserRepository(ApplicationDbContext context, IAggregateFactory<User, UserModel> factory,
        IModelFactory<UserModel, User> modelFactory)
    {
        _context = context;
        _factory = factory;
        _modelFactory = modelFactory;
    }

    public async Task<User?> GetAsync(Guid id)
    {
        var model = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        return model == null ? null : _factory.Create(model);
    }

    public async Task AddAsync(User entity)
    {
        var model = await _modelFactory.CreateAsync(entity);
        _context.Add(model);
    }

    public async Task UpdateAsync(User entity)
    {
        var model = await _modelFactory.CreateAsync(entity);
        _context.Update(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (model != null) _context.Remove(model);
    }

    public async Task DeleteAsync(ISpecification<User, IUserSpecificationVisitor> specification)
    {
        var query = _context.Users.AsQueryable();
        var visitor = new UserVisitor();
        specification.Accept(visitor);
        if (visitor.Expr != null) query = query.Where(visitor.Expr);
        _context.RemoveRange(await query.ToListAsync());
    }

    public Task<int> CountAsync(ISpecification<User, IUserSpecificationVisitor>? specification)
    {
        throw new NotImplementedException();
    }

    public Task<IList<User>> FindAsync(ISpecification<User, IUserSpecificationVisitor>? specification,
        IOrderBy<User, IUserSortingVisitor>? orderBy = null, int? skip = null, int? take = null)
    {
        throw new NotImplementedException();
    }
}