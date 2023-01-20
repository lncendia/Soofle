using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Participants.Specification;

public class ChildParticipantsSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public ChildParticipantsSpecification(Guid parentId) => ParentId = parentId;

    public Guid ParentId { get; }
    public bool IsSatisfiedBy(Participant item) => item.ParentParticipantId == ParentId;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}