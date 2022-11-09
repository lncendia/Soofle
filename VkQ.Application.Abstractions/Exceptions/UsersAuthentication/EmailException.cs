namespace VkQ.Application.Abstractions.Exceptions.UsersAuthentication;

public class EmailException : Exception
{
    public EmailException(Exception baseException) : base("Failed to send email.", baseException)
    {
    }
}