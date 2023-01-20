using Soofle.Domain.Reposts.ParticipantReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Ordering.Visitor;
using Soofle.Domain.Reposts.ParticipantReport.Specification.Visitor;

namespace Soofle.Domain.Abstractions.Repositories;

public interface IParticipantReportRepository : IRepository<ParticipantReport, IParticipantReportSpecificationVisitor, IParticipantReportSortingVisitor>
{
}