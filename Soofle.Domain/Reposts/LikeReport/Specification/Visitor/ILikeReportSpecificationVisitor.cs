using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Proxies.Specification;

namespace Soofle.Domain.Reposts.LikeReport.Specification.Visitor;

public interface ILikeReportSpecificationVisitor : ISpecificationVisitor<ILikeReportSpecificationVisitor, Entities.LikeReport>
{
}