namespace VkQ.Domain.Abstractions.Exceptions;

public class LinkAlreadyExistsException:Exception
{
    public Guid User1Id { get; }
    public Guid User2Id { get; }
    public LinkAlreadyExistsException(Guid user1Id, Guid user2Id) : base("Link already exists")
    {
        User1Id = user1Id;
        User2Id = user2Id;
    }

}