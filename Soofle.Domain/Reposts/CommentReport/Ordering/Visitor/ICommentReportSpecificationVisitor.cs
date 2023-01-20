using Soofle.Domain.Ordering.Abstractions;

namespace Soofle.Domain.Reposts.CommentReport.Ordering.Visitor;

public interface ICommentReportSortingVisitor : ISortingVisitor<ICommentReportSortingVisitor, Entities.CommentReport>
{
}