namespace VkQ.Domain.Abstractions.Exceptions;

public class LinkBetweenUsersNotFoundException : Exception
{
    public LinkBetweenUsersNotFoundException() : base("Link between users not found")
    {
    }
}