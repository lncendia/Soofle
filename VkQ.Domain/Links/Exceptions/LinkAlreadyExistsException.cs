namespace VkQ.Domain.Links.Exceptions;

public class LinkAlreadyExistsException : Exception
{
    public LinkAlreadyExistsException() : base("Link already exist")
    {
    }
}