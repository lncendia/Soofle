using Soofle.Domain.Links.Entities;
using Soofle.Domain.Links.Ordering.Visitor;
using Soofle.Domain.Links.Specification.Visitor;

namespace Soofle.Domain.Abstractions.Repositories;

public interface ILinkRepository:IRepository<Link, ILinkSpecificationVisitor, ILinkSortingVisitor>
{
    
}