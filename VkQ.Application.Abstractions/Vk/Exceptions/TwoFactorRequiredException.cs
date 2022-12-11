namespace VkQ.Application.Abstractions.Vk.Exceptions;

public class TwoFactorRequiredException:Exception
{
    public TwoFactorRequiredException():base("Two-factor authorization is required")
    {
    }
}