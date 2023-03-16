using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Transactions.Entities;

namespace Soofle.Domain.Transactions.Specification.Visitor;

public interface ITransactionSpecificationVisitor : ISpecificationVisitor<ITransactionSpecificationVisitor, Transaction>
{
    void Visit(TransactionByUserIdSpecification specification);
}