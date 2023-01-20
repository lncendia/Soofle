using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Enums;
using Soofle.Domain.Participants.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Participants.Specification;

public class ParticipantsByTypeSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public ParticipantsByTypeSpecification(ParticipantType type) => Type = type;

    public ParticipantType Type { get;}

    public bool IsSatisfiedBy(Participant item) => item.Type == Type;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}