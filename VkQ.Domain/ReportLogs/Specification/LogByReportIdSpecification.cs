using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Proxies.Specification.Visitor;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Domain.ReportLogs.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.ReportLogs.Specification;

public class LogByReportIdSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{
    public LogByReportIdSpecification(Guid id) => Id = id;

    public Guid Id { get; }
    public bool IsSatisfiedBy(ReportLog item) => item.UserId == Id;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}