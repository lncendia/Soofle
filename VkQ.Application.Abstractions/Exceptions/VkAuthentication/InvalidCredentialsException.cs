namespace VkQ.Application.Abstractions.Exceptions.VkAuthentication;

public class InvalidCredentialsException:Exception
{
    public InvalidCredentialsException():base("Invalid credentials")
    {
    }
}