using System.Linq.Expressions;
using Soofle.Infrastructure.DataStorage.Models.Reports.CommentReport;
using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.CommentReport.Specification.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Specifications;

internal class CommentReportVisitor : BaseVisitor<CommentReportModel, ICommentReportSpecificationVisitor, CommentReport>,
    ICommentReportSpecificationVisitor
{
    protected override Expression<Func<CommentReportModel, bool>> ConvertSpecToExpression(
        ISpecification<CommentReport, ICommentReportSpecificationVisitor> spec)
    {
        var visitor = new CommentReportVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }
}