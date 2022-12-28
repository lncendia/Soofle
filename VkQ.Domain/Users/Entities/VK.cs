using VkQ.Domain.Abstractions;
using VkQ.Domain.Proxies.Entities;

namespace VkQ.Domain.Users.Entities;

public class Vk : Entity
{
    internal Vk(string username, string password)
    {
        Login = username;
        Password = password;
    }

    public string Login { get; private set; }
    public string Password { get; private set; }
    public string? AccessToken { get; private set; }

    public Guid? ProxyId { get; private set; }

    internal void SetProxy(Proxy proxy) => ProxyId = proxy.Id;

    internal void UpdateToken(string token) => AccessToken = token;

    internal void UpdateData(string login, string password)
    {
        Login = login;
        Password = password;
        AccessToken = null;
    }

    public bool IsActive => !string.IsNullOrEmpty(AccessToken);
}