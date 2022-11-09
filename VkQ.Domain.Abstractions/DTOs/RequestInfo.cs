namespace VkQ.Domain.Abstractions.DTOs;

public class RequestInfo
{
    public RequestInfo(string token, string host, int port, string login, string password)
    {
        Token = token;
        Host = host;
        Port = port;
        Login = login;
        Password = password;
    }

    public string Token { get; }
    public string Host { get; }
    public int Port { get; }
    public string Login { get; }
    public string Password { get; }
}