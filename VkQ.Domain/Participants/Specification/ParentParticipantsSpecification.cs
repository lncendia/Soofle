using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Participants.Specification;

public class ParentParticipantsSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public bool IsSatisfiedBy(Participant item) => item.ParentParticipantId == null;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}