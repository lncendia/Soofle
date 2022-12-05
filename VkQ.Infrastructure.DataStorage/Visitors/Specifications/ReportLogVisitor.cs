using System.Linq.Expressions;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Domain.ReportLogs.Specification;
using VkQ.Domain.ReportLogs.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class ReportLogVisitor : BaseVisitor<ReportLogModel, IReportLogSpecificationVisitor, ReportLog>,
    IReportLogSpecificationVisitor
{
    protected override Expression<Func<ReportLogModel, bool>> ConvertSpecToExpression(
        ISpecification<ReportLog, IReportLogSpecificationVisitor> spec)
    {
        var visitor = new ReportLogVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }

    public void Visit(LogByCreationDateSpecification specification) => Expr = x =>
        x.CreatedAt >= specification.MinDate && x.CreatedAt <= specification.MaxDate;

    public void Visit(LogByUserIdSpecification specification) => Expr = x => x.UserId == specification.Id;

    public void Visit(LogByReportIdSpecification specification) => Expr = x => x.ReportId == specification.Id;
}