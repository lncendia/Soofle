using VkQ.Application.Abstractions.Proxies.Exceptions;
using VkQ.Application.Abstractions.Proxies.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Proxies.Ordering;
using VkQ.Domain.Users.Entities;

namespace VkQ.Application.Services.Services.Proxy;

public class ProxySetterService : IProxySetter
{
    private readonly IUnitOfWork _unitOfWork;

    public ProxySetterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    private async Task<Domain.Proxies.Entities.Proxy> GetProxyAsync()
    {
        var proxies = await _unitOfWork.ProxyRepository.Value.FindAsync(null, new ProxyByRandomOrder(), 0, 1);
        if (!proxies.Any()) throw new UnableFindProxyException();
        return proxies.First();
    }

    public async Task SetProxyAsync(User user)
    {
        var proxy = await GetProxyAsync();
        user.SetVkProxy(proxy);
    }
}