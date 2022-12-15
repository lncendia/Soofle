using VkQ.Domain.Abstractions;
using VkQ.Domain.Links.Exceptions;

namespace VkQ.Domain.Links.Entities;

public class Link : AggregateRoot
{
    /// <exception cref="SameUsersException"></exception>
    public Link(Guid userId, List<Link> allUsersLinks, Guid user2Id)
    {
        if (userId == user2Id) throw new SameUsersException();
        if (allUsersLinks.Any(x => x.User1Id != user2Id && x.User2Id != userId))
            throw new ArgumentException(null, nameof(allUsersLinks));
        if (allUsersLinks.Count >= 20) throw new TooManyLinksForUserException(User1Id);
        if (allUsersLinks.Any(x => x.User1Id == user2Id || x.User2Id == user2Id))
            throw new LinkAlreadyExistsException();
        User1Id = userId;
        User2Id = user2Id;
    }

    public Guid User1Id { get; }
    public Guid User2Id { get; }
    public bool IsConfirmed { get; private set; }

    public void Confirm()
    {
        if (IsConfirmed) throw new LinkAlreadyConfirmedException();
        IsConfirmed = true;
    }
}