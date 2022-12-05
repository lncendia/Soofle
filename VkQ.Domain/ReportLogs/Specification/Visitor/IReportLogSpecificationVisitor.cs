using VkQ.Domain.ReportLogs.Entities;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.ReportLogs.Specification.Visitor;

public interface IReportLogSpecificationVisitor : ISpecificationVisitor<IReportLogSpecificationVisitor, ReportLog>
{
    void Visit(LogByCreationDateSpecification specification);
    void Visit(LogByUserIdSpecification specification);
    void Visit(LogByReportIdSpecification specification);
}