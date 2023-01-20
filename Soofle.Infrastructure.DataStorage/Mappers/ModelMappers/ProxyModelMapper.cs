using Microsoft.EntityFrameworkCore;
using Soofle.Infrastructure.DataStorage.Context;
using Soofle.Infrastructure.DataStorage.Mappers.Abstractions;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.Proxies.Entities;

namespace Soofle.Infrastructure.DataStorage.Mappers.ModelMappers;

internal class ProxyModelMapper : IModelMapperUnit<ProxyModel, Proxy>
{
    private readonly ApplicationDbContext _context;

    public ProxyModelMapper(ApplicationDbContext context) => _context = context;

    public async Task<ProxyModel> MapAsync(Proxy model)
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