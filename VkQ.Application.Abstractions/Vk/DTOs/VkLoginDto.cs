namespace VkQ.Application.Abstractions.Vk.DTOs;

public class VkLoginDto
{
    public VkLoginDto(string login, string password, VkProxyDto proxy)
    {
        Login = login;
        Password = password;
        Proxy = proxy;
    }

    public string Login { get; }
    public string Password { get; }
    public VkProxyDto Proxy { get; }
}