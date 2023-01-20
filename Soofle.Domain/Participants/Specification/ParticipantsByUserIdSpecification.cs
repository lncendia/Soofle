using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Participants.Specification;

public class ParticipantsByUserIdSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public ParticipantsByUserIdSpecification(Guid userId) => UserId = userId;

    public Guid UserId { get; }

    public bool IsSatisfiedBy(Participant item) => item.UserId == UserId;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}