using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Domain.ReportLogs.Specification.Visitor;

public interface IReportLogSpecificationVisitor : ISpecificationVisitor<IReportLogSpecificationVisitor, ReportLog>
{
    void Visit(LogByCreationDateSpecification specification);
    void Visit(LogByCompletionDateSpecification specification);
    void Visit(LogByUserIdSpecification specification);
    void Visit(LogByReportIdSpecification specification);
    void Visit(LogByInfoSpecification specification);
    void Visit(LogByReportTypeSpecification specification);
    void Visit(LogByExistingReportSpecification specification);
}