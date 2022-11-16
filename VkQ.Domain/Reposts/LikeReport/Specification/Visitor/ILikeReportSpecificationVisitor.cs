using VkQ.Domain.Proxies.Specification;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Reposts.LikeReport.Specification.Visitor;

public interface ILikeReportSpecificationVisitor : ISpecificationVisitor<ILikeReportSpecificationVisitor, Entities.LikeReport>
{
}