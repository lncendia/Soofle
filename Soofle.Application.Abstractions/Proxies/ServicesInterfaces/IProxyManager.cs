using Soofle.Application.Abstractions.Proxies.DTOs;

namespace Soofle.Application.Abstractions.Proxies.ServicesInterfaces;

public interface IProxyManager
{
    public Task<List<ProxyDto>> FindAsync(int page, string? host);
    public Task AddAsync(string proxyList);
    public Task DeleteAsync(Guid id);
}