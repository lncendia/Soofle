namespace VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;

public class InvalidCodeException : Exception
{
    public InvalidCodeException() : base("Invalid code specified.")
    {
    }
}