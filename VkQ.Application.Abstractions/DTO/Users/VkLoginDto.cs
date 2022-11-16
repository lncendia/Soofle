namespace VkQ.Application.Abstractions.DTO.Users;

public class VkLoginDto
{
    public VkLoginDto(string login, string password, string proxyHost, int proxyPort, string proxyLogin,
        string proxyPassword)
    {
        Login = login;
        Password = password;
        Proxy = new ProxyDto(proxyPort, proxyHost, proxyLogin, proxyPassword);
    }

    public string Login { get; }
    public string Password { get; }
    public ProxyDto Proxy { get; }
}