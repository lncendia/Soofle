using VkQ.Application.Abstractions.Exceptions.Proxy;
using VkQ.Application.Abstractions.Interfaces.Proxy;
using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Proxies.Ordering;

namespace VkQ.Application.Services.Services.Proxy;

public class ProxySelectorService : IProxySelector
{
    private readonly IUnitOfWork _unitOfWork;

    public ProxySelectorService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Domain.Proxies.Entities.Proxy> GetProxyAsync()
    {
        var proxies = await _unitOfWork.ProxyRepository.Value.FindAsync(null, new ProxyByRandomOrder(), 0, 1);
        if (!proxies.Any()) throw new UnableFindProxyException();
        return proxies.First();
    }
}