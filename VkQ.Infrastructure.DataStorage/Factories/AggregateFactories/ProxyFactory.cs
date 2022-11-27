using System.Reflection;
using VkQ.Domain.Proxies.Entities;
using VkQ.Infrastructure.DataStorage.Factories.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Factories.AggregateFactories;

internal class ProxyFactory : IAggregateFactory<Proxy, ProxyModel>
{
    private static readonly FieldInfo ProxyId =
        typeof(Proxy).GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public Proxy Create(ProxyModel model)
    {
        var proxy = new Proxy(model.Host, model.Port, model.Login, model.Password);
        ProxyId.SetValue(proxy, model.Id);
        return proxy;
    }
}