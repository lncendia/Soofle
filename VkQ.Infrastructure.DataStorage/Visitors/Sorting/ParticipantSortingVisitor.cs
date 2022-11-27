using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Ordering.Visitor;
using VkQ.Infrastructure.DataStorage.Models;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class ParticipantSortingVisitor : BaseSortingVisitor<ParticipantModel, IParticipantSortingVisitor, Participant>, IParticipantSortingVisitor
{
    protected override List<SortData<ParticipantModel>> ConvertOrderToList(IOrderBy<Participant, IParticipantSortingVisitor> spec)
    {
        var visitor = new ParticipantSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}