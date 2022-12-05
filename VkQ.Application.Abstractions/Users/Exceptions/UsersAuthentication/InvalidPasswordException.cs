namespace VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base("Invalid password entered.")
    {
    }
}