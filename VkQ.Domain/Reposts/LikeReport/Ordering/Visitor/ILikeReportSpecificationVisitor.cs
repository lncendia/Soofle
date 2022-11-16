using VkQ.Domain.Ordering.Abstractions;

namespace VkQ.Domain.Reposts.LikeReport.Ordering.Visitor;

public interface ILikeReportSortingVisitor : ISortingVisitor<ILikeReportSortingVisitor, Entities.LikeReport>
{
}