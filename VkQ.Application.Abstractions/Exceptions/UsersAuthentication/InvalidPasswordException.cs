namespace VkQ.Application.Abstractions.Exceptions.UsersAuthentication;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base("Invalid password entered.")
    {
    }
}