using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.ReportLogs.Specification;

public class LogByReportIdSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{
    public LogByReportIdSpecification(Guid id) => Id = id;

    public Guid Id { get; }
    public bool IsSatisfiedBy(ReportLog item) => item.UserId == Id;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}