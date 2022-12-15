using VkQ.Domain.Links.Entities;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers.StaticMethods;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class LinkMapper : IAggregateMapperUnit<Link, LinkModel>
{
    public Link Map(LinkModel model)
    {
        var link = new Link(model.User1Id, new List<Link>(), model.User2Id);
        IdFields.AggregateId.SetValue(link, model.Id);
        if (model.IsAccepted) link.Confirm();
        return link;
    }
}