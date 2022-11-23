using System.Reflection;
using VkQ.Domain.Links.Entities;
using VkQ.Infrastructure.DataStorage.Factories.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Factories.AggregateFactories;

public class LinkFactory : IAggregateFactory<Link, LinkModel>
{
    private static readonly FieldInfo LinkId =
        typeof(Link).GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public Link Create(LinkModel model)
    {
        var link = new Link(model.User1Id, model.User2Id);
        LinkId.SetValue(link, model.Id);
        return link;
    }
}