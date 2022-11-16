namespace VkQ.Domain.Abstractions.Exceptions;

public class ProxyIsNotSetException:Exception
{
    public ProxyIsNotSetException():base("Proxy is not set")
    {
    }
}