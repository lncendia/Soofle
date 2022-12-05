namespace VkQ.Application.Abstractions.Users.Exceptions.VkAuthentication;

public class InvalidCredentialsException:Exception
{
    public InvalidCredentialsException():base("Invalid credentials")
    {
    }
}