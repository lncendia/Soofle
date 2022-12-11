using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Participants.Specification;

public class VipParticipantsSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public bool IsSatisfiedBy(Participant item) => item.Vip;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}