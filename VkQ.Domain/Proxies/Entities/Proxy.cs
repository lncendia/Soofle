using System.Text.RegularExpressions;
using VkQ.Domain.Proxies.Exceptions;

namespace VkQ.Domain.Proxies.Entities;

public class Proxy
{
    public Proxy(string host, int port, string login, string password)
    {
        if (port is >= 0 and <= 65536) Port = port;
        else throw new InvalidPortFormatException(port);
        if (Regex.IsMatch(host, @"((?:[a-z0-9\-]*\.){1,}[a-z0-9\-]*)")) Host = host;
        else throw new InvalidHostFormatException(host);
        Id = Guid.NewGuid();
        Login = login;
        Password = password;
    }

    public Guid Id { get; }

    public string Host { get; }


    public int Port { get; }

    public string Login { get; }
    public string Password { get; }
}