using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Participants.Entities;

namespace Soofle.Domain.Participants.Ordering.Visitor;

public interface IParticipantSortingVisitor : ISortingVisitor<IParticipantSortingVisitor, Participant>
{
}