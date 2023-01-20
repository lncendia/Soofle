using Soofle.Domain.Links.Entities;
using Soofle.Domain.Ordering.Abstractions;

namespace Soofle.Domain.Links.Ordering.Visitor;

public interface ILinkSortingVisitor : ISortingVisitor<ILinkSortingVisitor, Link>
{
}