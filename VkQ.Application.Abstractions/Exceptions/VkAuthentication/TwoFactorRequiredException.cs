namespace VkQ.Application.Abstractions.Exceptions.VkAuthentication;

public class TwoFactorRequiredException:Exception
{
    public TwoFactorRequiredException():base("Two-factor authorization is required")
    {
    }
}