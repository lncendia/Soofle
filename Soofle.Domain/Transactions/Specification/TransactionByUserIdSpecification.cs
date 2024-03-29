using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Transactions.Entities;
using Soofle.Domain.Transactions.Specification.Visitor;

namespace Soofle.Domain.Transactions.Specification;

public class TransactionByUserIdSpecification : ISpecification<Transaction, ITransactionSpecificationVisitor>
{
    public Guid Id { get; }
    public TransactionByUserIdSpecification(Guid id) => Id = id;

    public bool IsSatisfiedBy(Transaction item) => item.UserId == Id;

    public void Accept(ITransactionSpecificationVisitor visitor) => visitor.Visit(this);
}