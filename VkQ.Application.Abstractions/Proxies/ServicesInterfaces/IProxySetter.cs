using VkQ.Application.Abstractions.Proxies.Exceptions;
using VkQ.Domain.Users.Entities;

namespace VkQ.Application.Abstractions.Proxies.ServicesInterfaces;

public interface IProxySetter
{
    /// <exception cref="UnableFindProxyException"></exception>
    Task SetProxyAsync(User user);
}