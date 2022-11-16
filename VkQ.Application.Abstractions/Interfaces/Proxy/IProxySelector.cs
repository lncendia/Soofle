namespace VkQ.Application.Abstractions.Interfaces.Proxy;

public interface IProxySelector
{
    Task<Domain.Proxies.Entities.Proxy> GetProxyAsync();
}