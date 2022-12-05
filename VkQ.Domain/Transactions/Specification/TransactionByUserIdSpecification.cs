using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Transactions.Entities;
using VkQ.Domain.Transactions.Specification.Visitor;

namespace VkQ.Domain.Transactions.Specification;

public class TransactionByUserIdSpecification : ISpecification<Transaction, ITransactionSpecificationVisitor>
{
    public Guid Id { get; }
    public TransactionByUserIdSpecification(Guid id) => Id = id;

    public bool IsSatisfiedBy(Transaction item) => item.UserId == Id;

    public void Accept(ITransactionSpecificationVisitor visitor) => visitor.Visit(this);
}