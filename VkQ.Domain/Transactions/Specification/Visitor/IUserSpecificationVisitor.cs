using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Transactions.Entities;
using VkQ.Domain.Users.Specification;

namespace VkQ.Domain.Transactions.Specification.Visitor;

public interface ITransactionSpecificationVisitor : ISpecificationVisitor<ITransactionSpecificationVisitor, Transaction>
{
    void Visit(TransactionByUserIdSpecification specification);
}