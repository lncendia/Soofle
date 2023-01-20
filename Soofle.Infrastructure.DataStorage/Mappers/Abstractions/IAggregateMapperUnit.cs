using Soofle.Domain.Abstractions;
using Soofle.Infrastructure.DataStorage.Models.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Mappers.Abstractions;

internal interface IAggregateMapperUnit<out TAggregate, in TModel> where TAggregate : AggregateRoot where TModel : IAggregateModel
{
    TAggregate Map(TModel model);
}