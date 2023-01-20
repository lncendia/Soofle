using Soofle.Application.Abstractions.Proxies.DTOs;
using Soofle.Application.Abstractions.Proxies.Exceptions;
using Soofle.Application.Abstractions.Proxies.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Proxies.Exceptions;
using Soofle.Domain.Proxies.Specification;

namespace Soofle.Application.Services.Proxies;

public class ProxyManager : IProxyManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ProxyManager(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<List<ProxyDto>> FindAsync(int page, string? host)
    {
        var spec = string.IsNullOrEmpty(host) ? null : new ProxyByHostSpecification(host);
        var proxies = await _unitOfWork.ProxyRepository.Value.FindAsync(spec, null, (page - 1) * 20, 20);
        return proxies.Select(x => new ProxyDto(x.Id, x.Host, x.Port, x.Login, x.Password)).ToList();
    }

    public async Task AddAsync(string proxyList)
    {
        var proxies = ParseProxyList(proxyList);
        foreach (var proxy in proxies) await _unitOfWork.ProxyRepository.Value.AddAsync(proxy);
        await _unitOfWork.SaveChangesAsync();
    }

    private static List<Soofle.Domain.Proxies.Entities.Proxy> ParseProxyList(string proxyList)
    {
        var proxies = new List<Soofle.Domain.Proxies.Entities.Proxy>();
        var lines = proxyList.Split(Environment.NewLine);
        foreach (var line in lines)
        {
            var data = line.Split(':', 4);
            if (data.Length != 4) throw new ProxyListParseException(line);
            try
            {
                var proxy = new Soofle.Domain.Proxies.Entities.Proxy(data[0], int.Parse(data[1]), data[2], data[3]);
                proxies.Add(proxy);
            }
            catch (InvalidHostFormatException)
            {
                throw new ProxyListParseException(line);
            }
            catch (InvalidPortFormatException)
            {
                throw new ProxyListParseException(line);
            }
        }

        return proxies;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.ProxyRepository.Value.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}