using VkQ.Application.Abstractions.Proxies.Exceptions;
using VkQ.Application.Abstractions.Proxies.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Proxies.Ordering;
using VkQ.Domain.Users.Entities;

namespace VkQ.Application.Services.Services.Proxy;

public class ProxyGetterService : IProxyGetter
{
    private readonly IUnitOfWork _unitOfWork;

    public ProxyGetterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Domain.Proxies.Entities.Proxy> GetAsync()
    {
        var proxies = await _unitOfWork.ProxyRepository.Value.FindAsync(null, new ProxyByRandomOrder(), 0, 1);
        if (!proxies.Any()) throw new UnableFindProxyException();
        return proxies.First();
    }
}