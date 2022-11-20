namespace VkQ.Domain.Abstractions.Exceptions;

public class LinkNotFoundException : Exception
{
    public LinkNotFoundException() : base("Link between users not found")
    {
    }
}