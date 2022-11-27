using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.LikeReport.Ordering.Visitor;
using VkQ.Infrastructure.DataStorage.Models.Reports.LikeReport;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class LikeReportSortingVisitor : BaseSortingVisitor<LikeReportModel, ILikeReportSortingVisitor, LikeReport>, ILikeReportSortingVisitor
{
    protected override List<SortData<LikeReportModel>> ConvertOrderToList(IOrderBy<LikeReport, ILikeReportSortingVisitor> spec)
    {
        var visitor = new LikeReportSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}