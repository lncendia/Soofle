using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Proxies.Ordering.Visitor;
using VkQ.Domain.Proxies.Specification.Visitor;
using VkQ.Domain.Reposts.LikeReport;

namespace VkQ.Domain.Abstractions.Repositories;

public interface ILikeReportRepository : IRepository<LikeReport, ILikeReportSpecificationVisitor, ILikeReportSortingVisitor>
{
}