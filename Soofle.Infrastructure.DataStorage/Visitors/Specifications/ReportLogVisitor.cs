using System.Linq.Expressions;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Specification;
using Soofle.Domain.ReportLogs.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Visitors.Specifications;

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

    public void Visit(LogByCompletionDateSpecification specification) =>
        Expr = x =>
            x.FinishedAt >= specification.MinDate && x.FinishedAt <= specification.MaxDate;

    public void Visit(LogByUserIdSpecification specification) => Expr = x => x.UserId == specification.Id;

    public void Visit(LogByReportIdSpecification specification) => Expr = x => x.ReportId == specification.Id;
    public void Visit(LogByInfoSpecification specification) => Expr = x => x.AdditionalInfo == specification.Hashtag;

    public void Visit(LogByReportTypeSpecification specification) => Expr = x => x.Type == specification.Type;

    public void Visit(LogByExistingReportSpecification specification) =>
        Expr = x => x.ReportId.HasValue == specification.IsExist;
}