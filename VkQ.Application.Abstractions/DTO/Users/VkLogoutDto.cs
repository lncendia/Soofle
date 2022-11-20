using VkQ.Application.Abstractions.DTO;

namespace VkQ.Application.Abstractions.DTO.Users;

public class VkLogoutDto
{
    public VkLogoutDto(string proxyHost, int proxyPort, string proxyLogin, string proxyPassword, string token)
    {
        Proxy = new ProxyDto(proxyPort, proxyHost, proxyLogin, proxyPassword);
        Token = token;
    }
    
    public string Token { get; }
    public ProxyDto Proxy { get; }
}