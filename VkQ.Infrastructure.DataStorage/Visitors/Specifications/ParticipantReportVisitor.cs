using System.Linq.Expressions;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Specification.Visitor;
using VkQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

namespace VkQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class ParticipantReportVisitor : BaseVisitor<ParticipantReportModel, IParticipantReportSpecificationVisitor, ParticipantReport>, IParticipantReportSpecificationVisitor
{
    protected override Expression<Func<ParticipantReportModel, bool>> ConvertSpecToExpression(
        ISpecification<ParticipantReport, IParticipantReportSpecificationVisitor> spec)
    {
        var visitor = new ParticipantReportVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }
}