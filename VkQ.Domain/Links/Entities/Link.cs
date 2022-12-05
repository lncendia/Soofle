using VkQ.Domain.Abstractions;
using VkQ.Domain.Links.Exceptions;

namespace VkQ.Domain.Links.Entities;

public class Link : AggregateRoot
{
    /// <exception cref="SameUsersException"></exception>
    public Link(Guid user1Id, int user1LinksCount, Guid user2Id)
    {
        if (user1Id == user2Id) throw new SameUsersException();
        if (user1LinksCount < 0) throw new ArgumentOutOfRangeException(nameof(user1LinksCount));
        if (user1LinksCount >= 20) throw new TooManyLinksForUserException(User1Id);
        User1Id = user1Id;
        User2Id = user2Id;
    }

    public Guid User1Id { get; }
    public Guid User2Id { get; }
    public bool IsConfirmed { get; private set; }

    public void Confirm(int user2LinksCount)
    {
        if (IsConfirmed) throw new LinkAlreadyConfirmedException();
        if (user2LinksCount < 0) throw new ArgumentOutOfRangeException(nameof(user2LinksCount));
        if (user2LinksCount >= 20) throw new TooManyLinksForUserException(User2Id);
        IsConfirmed = true;
    }
}