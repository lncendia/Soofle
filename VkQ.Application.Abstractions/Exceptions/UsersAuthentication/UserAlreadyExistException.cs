namespace VkQ.Application.Abstractions.Exceptions.UsersAuthentication;

public class UserAlreadyExistException : Exception
{
    public UserAlreadyExistException() : base("The user is already registered.")
    {
    }
}