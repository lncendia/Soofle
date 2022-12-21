namespace VkQ.Domain.Reposts.BaseReport.Exceptions;

public class UserVkException : Exception
{
    public UserVkException(Guid userId) : base("User vk is not in valid state")
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}