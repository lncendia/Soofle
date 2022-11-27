using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Domain.ReportLogs.Ordering.Visitor;
using VkQ.Infrastructure.DataStorage.Models;
using VkQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class ReportLogSortingVisitor : BaseSortingVisitor<ReportLogModel, IReportLogSortingVisitor, ReportLog>, IReportLogSortingVisitor
{
    protected override List<SortData<ReportLogModel>> ConvertOrderToList(IOrderBy<ReportLog, IReportLogSortingVisitor> spec)
    {
        var visitor = new ReportLogSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
}