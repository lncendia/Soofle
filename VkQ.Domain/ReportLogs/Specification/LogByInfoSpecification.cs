using VkQ.Domain.ReportLogs.Entities;
using VkQ.Domain.ReportLogs.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.ReportLogs.Specification;

public class LogByInfoSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{
    public LogByInfoSpecification(string hashtag) => Hashtag = hashtag;

    public string Hashtag { get; }
    public bool IsSatisfiedBy(ReportLog item) => item.AdditionalInfo == Hashtag;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}