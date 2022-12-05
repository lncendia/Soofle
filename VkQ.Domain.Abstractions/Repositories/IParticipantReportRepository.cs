using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Ordering.Visitor;
using VkQ.Domain.Reposts.ParticipantReport.Specification.Visitor;

namespace VkQ.Domain.Abstractions.Repositories;

public interface IParticipantReportRepository : IRepository<ParticipantReport, IParticipantReportSpecificationVisitor, IParticipantReportSortingVisitor>
{
}