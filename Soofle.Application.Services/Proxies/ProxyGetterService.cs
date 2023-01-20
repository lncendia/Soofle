using Soofle.Application.Abstractions.Proxies.Exceptions;
using Soofle.Application.Abstractions.Proxies.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Proxies.Ordering;

namespace Soofle.Application.Services.Proxies;

public class ProxyGetterService : IProxyGetter
{
    private readonly IUnitOfWork _unitOfWork;

    public ProxyGetterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Soofle.Domain.Proxies.Entities.Proxy> GetAsync()
    {
        var proxies = await _unitOfWork.ProxyRepository.Value.FindAsync(null, new ProxyByRandomOrder(), 0, 1);
        if (!proxies.Any()) throw new UnableFindProxyException();
        return proxies.First();
    }
}