using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Participants.Specification.Visitor;

public interface IParticipantSpecificationVisitor : ISpecificationVisitor<IParticipantSpecificationVisitor, Participant>
{
    void Visit(ParticipantsByUserIdSpecification specification);
    void Visit(ParticipantsByNameSpecification specification);
    void Visit(ParticipantsByTypeSpecification specification);
    void Visit(VipParticipantsSpecification specification);
    void Visit(ParentParticipantsSpecification specification);
    void Visit(ChildParticipantsSpecification specification);
}