using VkQ.Application.Abstractions.Links.DTOs;
using VkQ.Application.Abstractions.Links.Exceptions;
using VkQ.Application.Abstractions.Links.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Links.Entities;
using VkQ.Domain.Links.Specification;
using VkQ.Domain.Users.Specification;

namespace VkQ.Application.Services.Services.Links;

public class LinkManager : ILinkManager
{
    private readonly IUnitOfWork _unitOfWork;

    public LinkManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddLinkDto> AddAsync(Guid user1, string email)
    {
        var user = (await _unitOfWork.UserRepository.Value.FindAsync(new UserByEmailSpecification(email), null, 0, 1))
            .FirstOrDefault();
        if (user == null) throw new UserNotFoundException();
        var links = await _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdSpecification(user1));
        var link = new Link(user1, links, user.Id);
        await _unitOfWork.LinkRepository.Value.AddAsync(link);
        await _unitOfWork.SaveChangesAsync();
        return new AddLinkDto(link.Id, user.Name);
    }

    public async Task DeleteAsync(Guid user1, Guid user2)
    {
        var link = await _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdsSpecification(user1, user2), null,
            0, 1);
        if (!link.Any()) throw new LinkNotFoundException();
        await _unitOfWork.LinkRepository.Value.DeleteAsync(link.First().Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AcceptAsync(Guid user1, Guid user2)
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