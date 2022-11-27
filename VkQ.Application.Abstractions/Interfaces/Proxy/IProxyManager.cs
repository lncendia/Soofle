using VkQ.Application.Abstractions.DTO.Proxy;

namespace VkQ.Application.Abstractions.Interfaces.Proxy;

public interface IProxyManager
{
    public Task<List<ProxyDto>> GetProxiesAsync();
    public Task AddProxyAsync(ProxyDto proxy);
}