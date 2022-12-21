using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Proxies.Specification.Visitor;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Domain.ReportLogs.Enums;
using VkQ.Domain.ReportLogs.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.ReportLogs.Specification;

public class LogByReportTypeSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{

    public ReportType Type { get; }

    public LogByReportTypeSpecification(ReportType type) => Type = type;

    public bool IsSatisfiedBy(ReportLog item) => item.Type == Type;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}