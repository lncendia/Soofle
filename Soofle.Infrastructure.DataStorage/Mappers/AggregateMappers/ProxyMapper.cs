using Soofle.Infrastructure.DataStorage.Mappers.Abstractions;
using Soofle.Infrastructure.DataStorage.Mappers.StaticMethods;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.Proxies.Entities;

namespace Soofle.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class ProxyMapper : IAggregateMapperUnit<Proxy, ProxyModel>
{
    public Proxy Map(ProxyModel model)
    {
        var proxy = new Proxy(model.Host, model.Port, model.Login, model.Password);
        IdFields.AggregateId.SetValue(proxy, model.Id);
        return proxy;
    }
}