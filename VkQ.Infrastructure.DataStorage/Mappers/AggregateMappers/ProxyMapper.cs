using VkQ.Domain.Proxies.Entities;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers.StaticMethods;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class ProxyMapper : IAggregateMapperUnit<Proxy, ProxyModel>
{
    public Proxy Map(ProxyModel model)
    {
        var proxy = new Proxy(model.Host, model.Port, model.Login, model.Password);
        IdFields.AggregateId.SetValue(proxy, model.Id);
        return proxy;
    }
}