using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Participants.Specification;

public class ParticipantsByNameSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public ParticipantsByNameSpecification(string name) => Name = name;

    public string Name { get; }

    public bool IsSatisfiedBy(Participant item) => item.Name.Contains(Name);

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}