using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.ReportLogs.Specification;

public class LogByInfoSpecification : ISpecification<ReportLog, IReportLogSpecificationVisitor>
{
    public LogByInfoSpecification(string hashtag) => Hashtag = hashtag;

    public string Hashtag { get; }
    public bool IsSatisfiedBy(ReportLog item) => item.AdditionalInfo == Hashtag;

    public void Accept(IReportLogSpecificationVisitor visitor) => visitor.Visit(this);
}