using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Proxies.Entities;
using Soofle.Domain.Proxies.Specification.Visitor;

namespace Soofle.Domain.ReportLogs.Specification;

public class LogByCompletionDateSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{
    public LogByCompletionDateSpecification(DateTimeOffset maxDate, DateTimeOffset minDate)
    {
        MaxDate = maxDate;
        MinDate = minDate;
    }

    public DateTimeOffset MaxDate { get; }
    public DateTimeOffset MinDate { get; }
    public bool IsSatisfiedBy(ReportLog item) => item.FinishedAt >= MinDate && item.CreatedAt <= MaxDate;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}