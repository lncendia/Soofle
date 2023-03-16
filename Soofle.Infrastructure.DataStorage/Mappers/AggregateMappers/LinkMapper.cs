﻿using Soofle.Infrastructure.DataStorage.Mappers.Abstractions;
using Soofle.Infrastructure.DataStorage.Mappers.StaticMethods;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.Links.Entities;

namespace Soofle.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class LinkMapper : IAggregateMapperUnit<Link, LinkModel>
{
    // ReSharper disable once CollectionNeverUpdated.Local
    private static readonly List<Link> MockList = new();
    public Link Map(LinkModel model)
    {
        var link = new Link(model.User1Id, MockList, model.User2Id);
        IdFields.AggregateId.SetValue(link, model.Id);
        if (model.IsAccepted) link.Confirm(MockList);
        return link;
    }
}