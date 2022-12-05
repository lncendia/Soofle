namespace VkQ.Application.Abstractions.Users.DTOs;

public class VkLogoutDto
{
    public VkLogoutDto(string token, VkProxyDto proxy)
    {
        Proxy = proxy;
        Token = token;
    }

    public string Token { get; }
    public VkProxyDto Proxy { get; }
}