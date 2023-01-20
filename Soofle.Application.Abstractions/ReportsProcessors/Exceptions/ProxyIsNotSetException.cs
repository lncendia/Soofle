namespace Soofle.Application.Abstractions.ReportsProcessors.Exceptions;

public class ProxyIsNotSetException:Exception
{
    public ProxyIsNotSetException():base("Proxy is not set")
    {
    }
}