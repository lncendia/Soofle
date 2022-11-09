using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Participants.Entities;

namespace VkQ.Domain.Participants.Ordering.Visitor;

public interface IParticipantSortingVisitor : ISortingVisitor<IParticipantSortingVisitor, Participant>
{
}