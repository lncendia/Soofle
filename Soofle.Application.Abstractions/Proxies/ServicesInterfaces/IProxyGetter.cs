using Soofle.Application.Abstractions.Proxies.Exceptions;
using Soofle.Domain.Proxies.Entities;

namespace Soofle.Application.Abstractions.Proxies.ServicesInterfaces;

public interface IProxyGetter
{
    /// <exception cref="UnableFindProxyException"></exception>
    Task<Proxy> GetAsync();
}