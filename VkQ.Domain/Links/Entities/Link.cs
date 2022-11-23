using VkQ.Domain.Links.Exceptions;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Links.Entities;

public class Link : IAggregateRoot
{
    public Link(Guid user1Id, Guid user2Id)
    {
        Id = Guid.NewGuid();
        if (user1Id == user2Id) throw new SameUsersException();
        User1Id = user1Id;
        User2Id = user2Id;
    }

    public Guid Id { get; }
    public Guid User1Id { get; }
    public Guid User2Id { get; }
}