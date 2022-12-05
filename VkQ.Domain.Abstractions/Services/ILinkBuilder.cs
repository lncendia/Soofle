using VkQ.Domain.Links.Entities;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Abstractions.Services;

public interface ILinkBuilder
{
    public Task<Link> BuildAsync(Guid user1, Guid user2);
}