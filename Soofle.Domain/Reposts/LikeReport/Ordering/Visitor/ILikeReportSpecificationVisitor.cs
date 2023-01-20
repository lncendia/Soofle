using Soofle.Domain.Ordering.Abstractions;

namespace Soofle.Domain.Reposts.LikeReport.Ordering.Visitor;

public interface ILikeReportSortingVisitor : ISortingVisitor<ILikeReportSortingVisitor, Entities.LikeReport>
{
}