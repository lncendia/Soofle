using VkQ.Domain;
using VkQ.Domain.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.Abstractions;

internal interface IAggregateMapper<out TAggregate, in TModel> where TAggregate : AggregateRoot where TModel : IModel
{
    TAggregate Map(TModel model);
}