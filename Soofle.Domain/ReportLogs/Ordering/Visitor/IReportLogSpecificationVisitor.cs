using Soofle.Domain.Ordering.Abstractions;

namespace Soofle.Domain.ReportLogs.Ordering.Visitor;

public interface IReportLogSortingVisitor : ISortingVisitor<IReportLogSortingVisitor, Entities.ReportLog>
{
    void Visit(LogByDateOrder order);
}