using System.Linq.Expressions;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.Transactions.Entities;
using Soofle.Domain.Transactions.Specification;
using Soofle.Domain.Transactions.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Visitors.Specifications;

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