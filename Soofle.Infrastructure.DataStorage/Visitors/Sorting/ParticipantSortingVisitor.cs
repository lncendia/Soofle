using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting.Models;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Ordering.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Sorting;

internal class ParticipantSortingVisitor : BaseSortingVisitor<ParticipantModel, IParticipantSortingVisitor, Participant>, IParticipantSortingVisitor
{
    protected override List<SortData<ParticipantModel>> ConvertOrderToList(IOrderBy<Participant, IParticipantSortingVisitor> spec)
    {
        var visitor = new ParticipantSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}