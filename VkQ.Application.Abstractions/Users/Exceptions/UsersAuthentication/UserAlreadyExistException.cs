namespace VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;

public class UserAlreadyExistException : Exception
{
    public UserAlreadyExistException() : base("The user is already registered.")
    {
    }
}