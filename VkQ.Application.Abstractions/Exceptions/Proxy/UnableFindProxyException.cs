namespace VkQ.Domain.Abstractions.Exceptions;

public class UnableFindProxyException : Exception
{
    public UnableFindProxyException() : base("Unable to find suitable proxy")
    {
    }
}