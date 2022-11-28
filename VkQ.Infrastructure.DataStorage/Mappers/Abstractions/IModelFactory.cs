using VkQ.Domain;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.Abstractions;

internal interface IModelMapper<TModel, in TAggregate> where TAggregate : IAggregateRoot where TModel : IModel
{
    Task<TModel> MapAsync(TAggregate model);
}