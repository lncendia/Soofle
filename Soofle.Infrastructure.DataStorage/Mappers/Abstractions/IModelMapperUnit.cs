using Soofle.Domain.Abstractions;
using Soofle.Infrastructure.DataStorage.Models.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Mappers.Abstractions;

internal interface IModelMapperUnit<TModel, in TAggregate> where TAggregate : AggregateRoot where TModel : IAggregateModel
{
    Task<TModel> MapAsync(TAggregate model);
}