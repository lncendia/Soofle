using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Reposts.ParticipantReport;

namespace VkQ.Domain.Abstractions.Repositories;

public interface IParticipantReportRepository : IRepository<ParticipantReport, IParticipantReportSpecificationVisitor, IParticipantReportSortingVisitor>
{
}