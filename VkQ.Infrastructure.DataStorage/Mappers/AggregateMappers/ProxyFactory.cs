using System.Reflection;
using VkQ.Domain.Proxies.Entities;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class ProxyMapper : IAggregateMapper<Proxy, ProxyModel>
{
    private static readonly FieldInfo ProxyId =
        typeof(Proxy).GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public Proxy Map(ProxyModel model)
    {
        var proxy = new Proxy(model.Host, model.Port, model.Login, model.Password);
        ProxyId.SetValue(proxy, model.Id);
        return proxy;
    }
}