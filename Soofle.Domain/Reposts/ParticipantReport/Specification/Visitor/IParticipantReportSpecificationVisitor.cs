using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Reposts.ParticipantReport.Specification.Visitor;

public interface
    IParticipantReportSpecificationVisitor : ISpecificationVisitor<IParticipantReportSpecificationVisitor,
        Entities.ParticipantReport>
{
}