using VkQ.Application.Abstractions.Links.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Links.Entities;
using VkQ.Domain.Links.Specification;
using VkQ.Domain.Links.Specification.Visitor;
using VkQ.Domain.Specifications;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Users.Specification;

namespace VkQ.Application.Services.Links;

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