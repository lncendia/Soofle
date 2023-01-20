using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.Participants.Specification;

public class VipParticipantsSpecification : ISpecification<Participant, IParticipantSpecificationVisitor>
{
    public bool IsSatisfiedBy(Participant item) => item.Vip;

    public void Accept(IParticipantSpecificationVisitor visitor) => visitor.Visit(this);
}