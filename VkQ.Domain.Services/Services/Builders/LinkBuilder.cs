using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Links.Entities;
using VkQ.Domain.Links.Specification;

namespace VkQ.Domain.Services.Services.Builders;

public class LinkBuilder : ILinkBuilder
{
    private readonly IUnitOfWork _unitOfWork;

    public LinkBuilder(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Link> BuildAsync(Guid user1, Guid user2)
    {
        var links1Count = await _unitOfWork.LinkRepository.Value.CountAsync(new LinkByUserIdSpecification(user1));
        var link = await _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdsSpecification(user1, user2));
        if (link is not null) throw new LinkAlreadyExistsException(user1, user2);
        return new Link(user1, links1Count, user2);
    }
}