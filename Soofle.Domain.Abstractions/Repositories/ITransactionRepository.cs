using Soofle.Domain.Transactions.Entities;
using Soofle.Domain.Transactions.Ordering.Visitor;
using Soofle.Domain.Transactions.Specification.Visitor;

namespace Soofle.Domain.Abstractions.Repositories;

public interface ITransactionRepository : IRepository<Transaction, ITransactionSpecificationVisitor, ITransactionSortingVisitor>
{
}