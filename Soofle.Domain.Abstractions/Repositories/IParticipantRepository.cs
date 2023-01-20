using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Ordering.Visitor;
using Soofle.Domain.Participants.Specification.Visitor;

namespace Soofle.Domain.Abstractions.Repositories;

public interface
    IParticipantRepository : IRepository<Participant, IParticipantSpecificationVisitor, IParticipantSortingVisitor>
{
}