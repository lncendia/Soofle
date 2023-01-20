using Soofle.Application.Abstractions.Links.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Links.Entities;
using Soofle.Domain.Links.Specification;
using Soofle.Domain.Links.Specification.Visitor;
using Soofle.Domain.Specifications;
using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Users.Specification;

namespace Soofle.Application.Services.Links;

public class UserLinksService : IUserLinksService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserLinksService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<List<(Guid id, string name)>> GetUserLinksAsync(Guid userId)
    {
        ISpecification<Link, ILinkSpecificationVisitor> linksSpec = new LinkByUserIdSpecification(userId);
        linksSpec = new AndSpecification<Link, ILinkSpecificationVisitor>(linksSpec, new AcceptedLinkSpecification());
        var links = await _unitOfWork.LinkRepository.Value.FindAsync(linksSpec);
        var usersIds = links.Select(x => x.User1Id == userId ? x.User2Id : x.User1Id).ToList();
        var usersSpec = new UserByIdsSpecification(usersIds);
        var users = await _unitOfWork.UserRepository.Value.FindAsync(usersSpec);
        return users.Select(x => (x.Id, x.Name)).ToList();
    }
}