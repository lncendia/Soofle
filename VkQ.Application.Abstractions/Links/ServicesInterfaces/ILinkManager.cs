using VkQ.Application.Abstractions.Links.DTOs;

namespace VkQ.Application.Abstractions.Links.ServicesInterfaces;

public interface ILinkManager
{
    Task<AddLinkDto> AddLinkAsync(Guid user1, string email);
    Task<List<LinkDto>> GetLinksAsync(Guid userId);
    Task RemoveLinkAsync(Guid user, Guid linkId);
    Task AcceptLinkAsync(Guid user, Guid linkId);
}