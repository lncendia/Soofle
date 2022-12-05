namespace VkQ.Application.Abstractions.Proxies.DTOs;

public class ProxyDto
{
    public ProxyDto(string host, int port, string login, string password)
    {
        Host = host;
        Port = port;
        Login = login;
        Password = password;
    }

    public string Host { get; }
    public int Port { get; }
    public string Login { get; }
    public string Password { get; }
}