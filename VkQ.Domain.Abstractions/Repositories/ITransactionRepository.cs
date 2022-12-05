using VkQ.Domain.Transactions.Entities;
using VkQ.Domain.Transactions.Ordering.Visitor;
using VkQ.Domain.Transactions.Specification.Visitor;

namespace VkQ.Domain.Abstractions.Repositories;

public interface ITransactionRepository : IRepository<Transaction, ITransactionSpecificationVisitor, ITransactionSortingVisitor>
{
}