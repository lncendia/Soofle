using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Domain.ReportLogs.Ordering.Visitor;
using VkQ.Domain.ReportLogs.Specification.Visitor;

namespace VkQ.Domain.Abstractions.Repositories;

public interface IReportLogRepository : IRepository<ReportLog, IReportLogSpecificationVisitor, IReportLogSortingVisitor>
{
}