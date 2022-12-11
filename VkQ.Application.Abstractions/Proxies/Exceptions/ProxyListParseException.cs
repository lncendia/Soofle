namespace VkQ.Application.Abstractions.Proxies.Exceptions;

public class ProxyListParseException : Exception
{
    public string Line { get; }
    public ProxyListParseException(string line) : base("Proxy list parse error")
    {
        Line = line;
    }
}