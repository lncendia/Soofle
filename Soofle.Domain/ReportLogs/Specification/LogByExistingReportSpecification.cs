using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.ReportLogs.Specification;

public class LogByExistingReportSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{
    public LogByExistingReportSpecification(bool isExist) => IsExist = isExist;
    public bool IsExist { get; }
    public bool IsSatisfiedBy(ReportLog item) => item.ReportId.HasValue == IsExist;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}