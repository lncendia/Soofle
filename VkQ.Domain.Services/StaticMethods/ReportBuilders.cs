using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Links.Entities;
using VkQ.Domain.Links.Specification;
using VkQ.Domain.Links.Specification.Visitor;
using VkQ.Domain.ReportLogs.Specification;
using VkQ.Domain.Specifications;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Services.StaticMethods;

internal static class ReportBuilders
{
    public static async Task CheckUserForCreate(IUnitOfWork uow, User user, List<Guid>? coAuthors)
    {
        if (coAuthors != null)
        {
            ISpecification<Link, ILinkSpecificationVisitor> spec =
                new AndSpecification<Link, ILinkSpecificationVisitor>(new LinkByUserIdSpecification(user.Id),
                    new LinkByUserIdSpecification(coAuthors.First()));

            spec = coAuthors.Skip(1).Aggregate(spec,
                (current, id) => new OrSpecification<Link, ILinkSpecificationVisitor>(current,
                    new AndSpecification<Link, ILinkSpecificationVisitor>(new LinkByUserIdSpecification(user.Id),
                        new LinkByUserIdSpecification(id))));

            if (await uow.LinkRepository.Value.CountAsync(spec) != coAuthors.Count) throw new LinkNotFoundException();
        }

        var reportsCountSpec = new LogByCreationDateSpecification(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(-1));
        if (await uow.ReportLogRepository.Value.CountAsync(reportsCountSpec) >= 25) throw new TooManyReportsException();
    }
}