namespace VkQ.Application.Abstractions.Vk.DTOs;

public class VkProxyDto
{
    public VkProxyDto(string host, int port, string login, string password)
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