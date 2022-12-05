using VkQ.Application.Abstractions.Links.DTOs;
using VkQ.Application.Abstractions.Links.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Links.Specification;
using VkQ.Domain.Specifications;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.Specification;
using VkQ.Domain.Users.Specification.Visitor;

namespace VkQ.Application.Services.Services.Links;

public class LinkManager : ILinkManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILinkBuilder _linkBuilder;

    public LinkManager(IUnitOfWork unitOfWork, ILinkBuilder linkBuilder)
    {
        _unitOfWork = unitOfWork;
        _linkBuilder = linkBuilder;
    }

    public async Task AddLinkAsync(Guid user1, Guid user2)
    {
        var link = await _linkBuilder.BuildAsync(user1, user2);
        await _unitOfWork.LinkRepository.Value.AddAsync(link);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<LinkDto>> GetLinksAsync(Guid userId)
    {
        var links = await _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdSpecification(userId));
        if (!links.Any()) return new List<LinkDto>();
        var ids = links.SelectMany(l => new[] { l.User1Id, l.User2Id }).Distinct().ToList();
        ISpecification<User, IUserSpecificationVisitor> spec = new UserByIdSpecification(ids.First());
        spec = ids.Skip(1).Aggregate(spec,
            (current, id) =>
                new OrSpecification<User, IUserSpecificationVisitor>(current, new UserByIdSpecification(id)));

        var users = await _unitOfWork.UserRepository.Value.FindAsync(spec);
        return links.Select(l => new LinkDto(l.User1Id, l.User2Id,
            users.FirstOrDefault(x => x.Id == l.Id)?.Name ?? throw new UserNotFoundException(),
            users.FirstOrDefault(x => x.Id == l.Id)?.Name ?? throw new UserNotFoundException(),
            l.IsConfirmed)).ToList();
    }

    public async Task RemoveLinkAsync(Guid user1, Guid user2)
    {
        var link = await _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdsSpecification(user1, user2), null,
            0, 1);
        if (!link.Any()) throw new LinkNotFoundException();
        await _unitOfWork.LinkRepository.Value.DeleteAsync(link.First().Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AcceptLinkAsync(Guid user1, Guid user2)
    {
        var links = await _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdsSpecification(user1, user2), null,
            0, 1);
        var link = links.FirstOrDefault(); 
        if (link == null) throw new LinkNotFoundException();
        link.Confirm();
        await _unitOfWork.LinkRepository.Value.UpdateAsync(link);
        await _unitOfWork.SaveChangesAsync();
    }
}