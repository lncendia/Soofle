using VkQ.Application.Abstractions.Links.DTOs;
using VkQ.Application.Abstractions.Links.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Links.Entities;
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

    public LinkManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddLinkDto> AddLinkAsync(Guid user1, string email)
    {
        var user = (await _unitOfWork.UserRepository.Value.FindAsync(new UserByEmailSpecification(email), null, 0, 1))
            .FirstOrDefault();
        if (user == null) throw new UserNotFoundException();
        var links1Count = await _unitOfWork.LinkRepository.Value.CountAsync(new LinkByUserIdSpecification(user1));
        var links = await _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdsSpecification(user1, user.Id));
        if (links.Any()) throw new LinkAlreadyExistsException(user1, user.Id);
        var link = new Link(user1, links1Count, user.Id);
        await _unitOfWork.LinkRepository.Value.AddAsync();
        await _unitOfWork.SaveChangesAsync();
        return new AddLinkDto(link.Id, user.Name);
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
        return links.Select(l =>
        {
            var user1 = new LinkDto.UserDto(l.User1Id,
                users.FirstOrDefault(x => x.Id == l.User1Id)?.Name ?? throw new UserNotFoundException());
            var user2 = new LinkDto.UserDto(l.User2Id,
                users.FirstOrDefault(x => x.Id == l.User2Id)?.Name ?? throw new UserNotFoundException());
            return new LinkDto(l.IsConfirmed, user1, user2);
        }).ToList();
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
        var t1 = _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdsSpecification(user1, user2), null,
            0, 1);
        var t2 = _unitOfWork.LinkRepository.Value.CountAsync(new LinkByUserIdSpecification(user2));
        await Task.WhenAll(t1, t2);
        var link = t1.Result.FirstOrDefault();
        if (link == null) throw new LinkNotFoundException();
        link.Confirm(t2.Result);
        await _unitOfWork.LinkRepository.Value.UpdateAsync(link);
        await _unitOfWork.SaveChangesAsync();
    }
}