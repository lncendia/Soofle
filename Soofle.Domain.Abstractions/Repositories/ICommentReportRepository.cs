using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.CommentReport.Ordering.Visitor;
using Soofle.Domain.Reposts.CommentReport.Specification.Visitor;

namespace Soofle.Domain.Abstractions.Repositories;

public interface ICommentReportRepository : IRepository<CommentReport, ICommentReportSpecificationVisitor, ICommentReportSortingVisitor>
{
}