using VkQ.Domain.Links.Exceptions;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Links.Entities;

public class Link
{
    public Link(User user1, User user2)
    {
        Id = Guid.NewGuid();
        if (user1.Id == user2.Id) throw new SameUsersException();
        User1Id = user1.Id;
        User2Id = user2.Id;
    }

    public Guid Id { get; }
    public Guid User1Id { get; }
    public Guid User2Id { get; }
}