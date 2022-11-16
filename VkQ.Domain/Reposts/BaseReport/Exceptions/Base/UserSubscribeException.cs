namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Base;

public class UserSubscribeException : Exception
{
    public UserSubscribeException() : base("User is not subscribed")
    {
    }
}