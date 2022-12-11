using VkQ.Application.Abstractions.Proxies.DTOs;
using VkQ.Application.Abstractions.Proxies.Exceptions;
using VkQ.Application.Abstractions.Proxies.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Proxies.Exceptions;
using VkQ.Domain.Proxies.Specification;

namespace VkQ.Application.Services.Services.Proxy;

public class ProxyManager : IProxyManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ProxyManager(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<List<ProxyDto>> FindAsync(int page, string? host)
    {
        var spec = string.IsNullOrEmpty(host) ? null : new ProxyByHostSpecification(host);
        var proxies = _unitOfWork.ProxyRepository.Value.FindAsync(spec, null, (page - 1) * 20, 20);
        var count = _unitOfWork.ProxyRepository.Value.CountAsync(spec);
        await Task.WhenAll(proxies, count);
        return proxies.Result.Select(x => new ProxyDto(x.Id, x.Host, x.Port, x.Login, x.Password)).ToList();
    }

    public async Task AddAsync(string proxyList)
    {
        var proxies = ParseProxyList(proxyList);
        var tasks = proxies.Select(x => _unitOfWork.ProxyRepository.Value.AddAsync(x));
        await Task.WhenAll(tasks);
        await _unitOfWork.SaveChangesAsync();
    }

    private static List<Domain.Proxies.Entities.Proxy> ParseProxyList(string proxyList)
    {
        var proxies = new List<Domain.Proxies.Entities.Proxy>();
        var lines = proxyList.Split(Environment.NewLine);
        foreach (var line in lines)
        {
            var data = line.Split(':', 4);
            if (data.Length != 4) throw new ProxyListParseException(line);
            try
            {
                var proxy = new Domain.Proxies.Entities.Proxy(data[0], int.Parse(data[1]), data[2], data[3]);
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