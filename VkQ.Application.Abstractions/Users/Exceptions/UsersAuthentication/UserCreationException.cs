namespace VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;

public class UserCreationException : Exception
{
    public UserCreationException(string error) : base($"Failed to create user: {error}")
    {
    }
}