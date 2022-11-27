using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Links.Entities;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Factories.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Factories.ModelFactories;

internal class LinkModelFactory : IModelFactory<LinkModel, Link>
{
    private readonly ApplicationDbContext _context;

    public LinkModelFactory(ApplicationDbContext context) => _context = context;

    public async Task<LinkModel> CreateAsync(Link model)
    {
        var link = await _context.Links.FirstOrDefaultAsync(x => x.Id == model.Id) ??
                   new LinkModel { Id = model.Id };
        link.User1Id = model.User1Id;
        link.User2Id = model.User2Id;
        return link;
    }
}