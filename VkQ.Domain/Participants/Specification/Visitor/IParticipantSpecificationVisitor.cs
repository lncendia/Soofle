using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Participants.Specification.Visitor;

public interface IParticipantSpecificationVisitor : ISpecificationVisitor<IParticipantSpecificationVisitor, Participant>
{
    void Visit(ParticipantsByUserIdSpecification specification);
}