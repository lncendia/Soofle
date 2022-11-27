using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Proxies.Entities;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Factories.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Factories.ModelFactories;

internal class ProxyModelFactory : IModelFactory<ProxyModel, Proxy>
{
    private readonly ApplicationDbContext _context;

    public ProxyModelFactory(ApplicationDbContext context) => _context = context;

    public async Task<ProxyModel> CreateAsync(Proxy model)
    {
        var proxy = await _context.Proxies.FirstOrDefaultAsync(x => x.Id == model.Id) ??
                   new ProxyModel { Id = model.Id };
        proxy.Host = model.Host;
        proxy.Port = model.Port;
        proxy.Login = model.Login;
        proxy.Password = model.Password;
        return proxy;
    }
}