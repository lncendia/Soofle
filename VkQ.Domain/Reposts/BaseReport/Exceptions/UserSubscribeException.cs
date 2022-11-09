namespace VkQ.Domain.Reposts.BaseReport.Exceptions;

public class UserSubscribeException:Exception
{
    public UserSubscribeException():base("User is not subscribed")
    {
    }
}