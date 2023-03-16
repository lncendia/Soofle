using System.Linq.Expressions;
using Soofle.Infrastructure.DataStorage.Models.Reports.LikeReport;
using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Specification.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Specifications;

internal class LikeReportVisitor : BaseVisitor<LikeReportModel, ILikeReportSpecificationVisitor, LikeReport>,
    ILikeReportSpecificationVisitor
{
    protected override Expression<Func<LikeReportModel, bool>> ConvertSpecToExpression(
        ISpecification<LikeReport, ILikeReportSpecificationVisitor> spec)
    {
        var visitor = new LikeReportVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }
}