using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Proxies.Specification.Visitor;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Domain.ReportLogs.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.ReportLogs.Specification;

public class LogByCreationDateSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{
    public LogByCreationDateSpecification(DateTimeOffset maxDate, DateTimeOffset minDate)
    {
        MaxDate = maxDate;
        MinDate = minDate;
    }

    public DateTimeOffset MaxDate { get; }
    public DateTimeOffset MinDate { get; }
    public bool IsSatisfiedBy(ReportLog item) => item.CreatedAt >= MinDate && item.CreatedAt <= MaxDate;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}