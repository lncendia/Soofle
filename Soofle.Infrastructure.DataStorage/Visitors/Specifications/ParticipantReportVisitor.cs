using System.Linq.Expressions;
using Soofle.Infrastructure.DataStorage.Models.Reports.ParticipantReport;
using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Reposts.ParticipantReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Specification.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Specifications;

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