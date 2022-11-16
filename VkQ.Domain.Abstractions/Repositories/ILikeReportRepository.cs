using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.LikeReport.Ordering.Visitor;
using VkQ.Domain.Reposts.LikeReport.Specification.Visitor;

namespace VkQ.Domain.Abstractions.Repositories;

public interface ILikeReportRepository : IRepository<LikeReport, ILikeReportSpecificationVisitor, ILikeReportSortingVisitor>
{
}