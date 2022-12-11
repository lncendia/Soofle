using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Enums;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Participants.Specification;

public class ParticipantsByTypeSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public ParticipantsByTypeSpecification(ParticipantType type) => Type = type;

    public ParticipantType Type { get;}

    public bool IsSatisfiedBy(Participant item) => item.Type == Type;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}