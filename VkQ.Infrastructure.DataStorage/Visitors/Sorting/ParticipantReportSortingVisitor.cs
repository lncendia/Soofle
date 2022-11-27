using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Ordering.Visitor;
using VkQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class ParticipantReportSortingVisitor : BaseSortingVisitor<ParticipantReportModel, IParticipantReportSortingVisitor, ParticipantReport>, IParticipantReportSortingVisitor
{
    protected override List<SortData<ParticipantReportModel>> ConvertOrderToList(IOrderBy<ParticipantReport, IParticipantReportSortingVisitor> spec)
    {
        var visitor = new ParticipantReportSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}