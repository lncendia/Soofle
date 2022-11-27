using System.Linq.Expressions;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.LikeReport.Specification.Visitor;
using VkQ.Infrastructure.DataStorage.Models.Reports.LikeReport;

namespace VkQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class LikeReportVisitor : BaseVisitor<LikeReportModel, ILikeReportSpecificationVisitor, LikeReport>, ILikeReportSpecificationVisitor
{
    protected override Expression<Func<LikeReportModel, bool>> ConvertSpecToExpression(
        ISpecification<LikeReport, ILikeReportSpecificationVisitor> spec)
    {
        var visitor = new LikeReportVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }
}