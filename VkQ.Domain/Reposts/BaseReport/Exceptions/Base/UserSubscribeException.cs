namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Base;

public class UserSubscribeException : Exception
{
    public UserSubscribeException(Guid userId, string name) : base("User is not subscribed")
    {
        UserId = userId;
        Name = name;
    }

    public Guid UserId { get; }
    public string Name { get; }
}