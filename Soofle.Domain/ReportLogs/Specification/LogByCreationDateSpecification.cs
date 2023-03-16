using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.ReportLogs.Specification;

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