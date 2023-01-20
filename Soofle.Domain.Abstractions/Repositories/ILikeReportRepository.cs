using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Ordering.Visitor;
using Soofle.Domain.Reposts.LikeReport.Specification.Visitor;

namespace Soofle.Domain.Abstractions.Repositories;

public interface ILikeReportRepository : IRepository<LikeReport, ILikeReportSpecificationVisitor, ILikeReportSortingVisitor>
{
}