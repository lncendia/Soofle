using Soofle.Infrastructure.DataStorage.Models.Reports.ParticipantReport;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting.Models;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Reposts.ParticipantReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Ordering.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Sorting;

internal class ParticipantReportSortingVisitor : BaseSortingVisitor<ParticipantReportModel, IParticipantReportSortingVisitor, ParticipantReport>, IParticipantReportSortingVisitor
{
    protected override List<SortData<ParticipantReportModel>> ConvertOrderToList(IOrderBy<ParticipantReport, IParticipantReportSortingVisitor> spec)
    {
        var visitor = new ParticipantReportSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}