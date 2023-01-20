using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Participants.Specification;

public class ParticipantsByNameSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public ParticipantsByNameSpecification(string name) => Name = name;

    public string Name { get; }

    public bool IsSatisfiedBy(Participant item) => item.Name.Contains(Name);

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}