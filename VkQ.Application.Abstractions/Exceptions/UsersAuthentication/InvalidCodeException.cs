namespace VkQ.Application.Abstractions.Exceptions.UsersAuthentication;

public class InvalidCodeException : Exception
{
    public InvalidCodeException() : base("Invalid code specified.")
    {
    }
}