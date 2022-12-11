namespace VkQ.Application.Abstractions.Vk.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("Invalid credentials")
    {
    }
}