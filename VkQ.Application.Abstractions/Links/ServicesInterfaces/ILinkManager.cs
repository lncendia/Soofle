using VkQ.Application.Abstractions.Links.DTOs;

namespace VkQ.Application.Abstractions.Links.ServicesInterfaces;

public interface ILinkManager
{
    Task AddLinkAsync(Guid user1, Guid user2);
    Task<List<LinkDto>> GetLinksAsync(Guid userId);
    Task RemoveLinkAsync(Guid user1, Guid user2);
    Task AcceptLinkAsync(Guid user1, Guid user2);
}