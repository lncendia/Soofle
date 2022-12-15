using VkQ.Application.Abstractions.Proxies.Exceptions;
using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Users.Entities;

namespace VkQ.Application.Abstractions.Proxies.ServicesInterfaces;

public interface IProxyGetter
{
    /// <exception cref="UnableFindProxyException"></exception>
    Task<Proxy> GetAsync();
}