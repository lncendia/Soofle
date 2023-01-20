using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Transactions.Entities;
using Soofle.Domain.Users.Specification;

namespace Soofle.Domain.Transactions.Specification.Visitor;

public interface ITransactionSpecificationVisitor : ISpecificationVisitor<ITransactionSpecificationVisitor, Transaction>
{
    void Visit(TransactionByUserIdSpecification specification);
}