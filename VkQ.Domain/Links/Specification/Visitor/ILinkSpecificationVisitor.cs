using VkQ.Domain.Links.Entities;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Domain.Links.Specification.Visitor;

public interface ILinkSpecificationVisitor : ISpecificationVisitor<ILinkSpecificationVisitor, Link>
{
    void Visit(LinkByUserIdSpecification specification);
}