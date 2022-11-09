namespace VkQ.Application.Abstractions.Exceptions.UsersAuthentication;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("Can't find user.")
    {
    }
}