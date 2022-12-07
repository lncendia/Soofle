using VkQ.Domain.Abstractions;
using VkQ.Domain.Links.Exceptions;

namespace VkQ.Domain.Links.Entities;

public class Link : AggregateRoot
{
    /// <exception cref="SameUsersException"></exception>
    public Link(Guid user1Id, List<Link> allUsersLinks, Guid user2Id)
    {
        //if (user1Id == user2Id) throw new SameUsersException();
        // if (allUsersLinks.Count >= 20) throw new TooManyLinksForUserException(User1Id);
        User1Id = user1Id;
        User2Id = user2Id;
    }

    public Guid User1Id { get; }
    public Guid User2Id { get; }
    public bool IsConfirmed { get; private set; }

    public void Confirm(List<Link> allUsers2Links)
    {
        if (IsConfirmed) throw new LinkAlreadyConfirmedException();
        //if (user2LinksCount < 0) throw new ArgumentOutOfRangeException(nameof(user2LinksCount));
        //if (user2LinksCount >= 20) throw new TooManyLinksForUserException(User2Id);
        IsConfirmed = true;
    }
}