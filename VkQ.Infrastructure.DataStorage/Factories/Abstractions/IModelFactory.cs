using VkQ.Domain;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Factories.Abstractions;

public interface IModelFactory<TModel, in TAggregate> where TAggregate : IAggregateRoot where TModel : IModel
{
    Task<TModel> CreateAsync(TAggregate model);
}