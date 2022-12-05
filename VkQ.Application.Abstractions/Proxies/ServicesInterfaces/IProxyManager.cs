using VkQ.Application.Abstractions.Proxies.DTOs;

namespace VkQ.Application.Abstractions.Proxies.ServicesInterfaces;

public interface IProxyManager
{
    public Task<List<ProxyDto>> GetProxiesAsync();
    public Task AddProxyAsync(ProxyDto proxy);
}