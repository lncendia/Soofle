using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Participants.Specification;

public class ParticipantsByUserIdSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public ParticipantsByUserIdSpecification(Guid userId) => UserId = userId;

    public Guid UserId { get; }

    public bool IsSatisfiedBy(Participant item) => item.UserId == UserId;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}