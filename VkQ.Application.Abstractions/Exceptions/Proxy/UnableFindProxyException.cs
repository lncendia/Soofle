namespace VkQ.Application.Abstractions.Exceptions.Proxy;

public class UnableFindProxyException : Exception
{
    public UnableFindProxyException() : base("Unable to find suitable proxy")
    {
    }
}