using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Participants.Specification;

public class ParentParticipantsSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public bool IsSatisfiedBy(Participant item) => item.ParentParticipantId == null;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}