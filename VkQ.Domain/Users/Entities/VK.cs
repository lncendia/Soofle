using VkQ.Domain.Abstractions;
using VkQ.Domain.Proxies.Entities;

namespace VkQ.Domain.Users.Entities;

public class Vk : Entity
{
    internal Vk(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
    public string Username { get; }
    public string Password { get; }
    public string? AccessToken { get; private set; }

    public Guid? ProxyId { get; private set; }

    internal void SetProxy(Proxy proxy) => ProxyId = proxy.Id;

    internal void UpdateToken(string token) => AccessToken = token;

    public bool IsActive() => !string.IsNullOrEmpty(AccessToken);
}