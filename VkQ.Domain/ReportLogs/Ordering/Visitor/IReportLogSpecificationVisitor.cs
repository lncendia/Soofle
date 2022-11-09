using VkQ.Domain.Ordering.Abstractions;

namespace VkQ.Domain.ReportLogs.Ordering.Visitor;

public interface IReportLogSortingVisitor : ISortingVisitor<IReportLogSortingVisitor, Entities.ReportLog>
{
}