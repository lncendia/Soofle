namespace VkQ.Application.Abstractions.Users.Exceptions.VkAuthentication;

public class TwoFactorRequiredException:Exception
{
    public TwoFactorRequiredException():base("Two-factor authorization is required")
    {
    }
}