using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Ordering.Visitor;
using Soofle.Domain.ReportLogs.Specification.Visitor;

namespace Soofle.Domain.Abstractions.Repositories;

public interface IReportLogRepository : IRepository<ReportLog, IReportLogSpecificationVisitor, IReportLogSortingVisitor>
{
}