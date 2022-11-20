using VkQ.Domain.Links.Entities;
using VkQ.Domain.Ordering.Abstractions;

namespace VkQ.Domain.Links.Ordering.Visitor;

public interface ILinkSortingVisitor : ISortingVisitor<ILinkSortingVisitor, Link>
{
}