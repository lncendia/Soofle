namespace VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("Can't find user.")
    {
    }
}