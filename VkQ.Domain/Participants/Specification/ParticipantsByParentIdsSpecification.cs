using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Participants.Specification;

public class ParticipantsByParentIdsSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public ParticipantsByParentIdsSpecification(Guid[] ids) => Ids = ids;

    public Guid[] Ids { get; }

    public bool IsSatisfiedBy(Participant item) =>
        item.ParentParticipantId.HasValue && Ids.Contains(item.ParentParticipantId.Value);

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}