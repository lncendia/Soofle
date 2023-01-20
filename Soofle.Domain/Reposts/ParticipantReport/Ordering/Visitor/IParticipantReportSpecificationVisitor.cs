using Soofle.Domain.Ordering.Abstractions;

namespace Soofle.Domain.Reposts.ParticipantReport.Ordering.Visitor;

public interface IParticipantReportSortingVisitor : ISortingVisitor<IParticipantReportSortingVisitor, Entities.ParticipantReport>
{
}