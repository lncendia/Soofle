using VkQ.Domain.Ordering.Abstractions;

namespace VkQ.Domain.Reposts.ParticipantReport.Ordering.Visitor;

public interface IParticipantReportSortingVisitor : ISortingVisitor<IParticipantReportSortingVisitor, Entities.ParticipantReport>
{
}