using VkQ.Domain;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.Abstractions;

internal interface IAggregateMapper<out TAggregate, in TModel> where TAggregate : IAggregateRoot where TModel : IModel
{
    TAggregate Map(TModel model);
}