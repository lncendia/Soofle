using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Ordering.Visitor;
using VkQ.Domain.Participants.Specification.Visitor;

namespace VkQ.Domain.Abstractions.Repositories;

public interface
    IParticipantRepository : IRepository<Participant, IParticipantSpecificationVisitor, IParticipantSortingVisitor>
{
}