using System.Linq.Expressions;
using VkQ.Domain.Transactions.Entities;
using VkQ.Domain.Transactions.Specification;
using VkQ.Domain.Transactions.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class TransactionVisitor : BaseVisitor<TransactionModel, ITransactionSpecificationVisitor, Transaction>,
    ITransactionSpecificationVisitor
{
    protected override Expression<Func<TransactionModel, bool>> ConvertSpecToExpression(
        ISpecification<Transaction, ITransactionSpecificationVisitor> spec)
    {
        var visitor = new TransactionVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }

    public void Visit(TransactionByUserIdSpecification specification) => Expr = x => x.UserId == specification.Id;
}