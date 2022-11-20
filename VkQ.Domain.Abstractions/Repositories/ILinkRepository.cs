using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Links.Entities;
using VkQ.Domain.Links.Ordering.Visitor;
using VkQ.Domain.Links.Specification.Visitor;

namespace VkQ.Domain.Abstractions.Repositories;

public interface ILinkRepository:IRepository<Link, ILinkSpecificationVisitor, ILinkSortingVisitor>
{
    
}