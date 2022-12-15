using VkQ.Domain.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.Abstractions;

internal interface IModelMapperUnit<TModel, in TAggregate> where TAggregate : AggregateRoot where TModel : IModel
{
    Task<TModel> MapAsync(TAggregate model);
}