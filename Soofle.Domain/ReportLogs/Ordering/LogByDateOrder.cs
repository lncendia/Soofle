using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Ordering.Visitor;

namespace Soofle.Domain.ReportLogs.Ordering;

public class LogByDateOrder : IOrderBy<ReportLog, IReportLogSortingVisitor>
{
    public IEnumerable<ReportLog> Order(IEnumerable<ReportLog> items) => items.OrderBy(x => x.CreatedAt);

    public IList<IEnumerable<ReportLog>> Divide(IEnumerable<ReportLog> items) =>
        Order(items).GroupBy(x => x.CreatedAt).Select(x => x.AsEnumerable()).ToList();

    public void Accept(IReportLogSortingVisitor visitor) => visitor.Visit(this);
}