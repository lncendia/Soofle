using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting.Models;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Ordering;
using Soofle.Domain.ReportLogs.Ordering.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Sorting;

internal class ReportLogSortingVisitor : BaseSortingVisitor<ReportLogModel, IReportLogSortingVisitor, ReportLog>,
    IReportLogSortingVisitor
{
    protected override List<SortData<ReportLogModel>> ConvertOrderToList(IOrderBy<ReportLog, IReportLogSortingVisitor> spec)
    {
        var visitor = new ReportLogSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }

    public void Visit(LogByDateOrder order) => SortItems.Add(new SortData<ReportLogModel>(x => x.CreatedAt, false));
}