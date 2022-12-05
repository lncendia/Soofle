namespace VkQ.Application.Abstractions.Proxies.Exceptions;

public class UnableFindProxyException : Exception
{
    public UnableFindProxyException() : base("Unable to find suitable proxy")
    {
    }
}