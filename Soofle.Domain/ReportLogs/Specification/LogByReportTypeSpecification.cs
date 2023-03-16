using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Enums;
using Soofle.Domain.ReportLogs.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.ReportLogs.Specification;

public class LogByReportTypeSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{

    public ReportType Type { get; }

    public LogByReportTypeSpecification(ReportType type) => Type = type;

    public bool IsSatisfiedBy(ReportLog item) => item.Type == Type;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}