using VkQ.Domain.Proxies.Entities;

namespace VkQ.Domain.Users.Entities;

public class Vk : IEntity
{
    public Vk(string username, string password)
    {
        Id = Guid.NewGuid();
        Username = username;
        Password = password;
    }

    public Guid Id { get; }

    public string Username { get; }
    public string Password { get; }
    public string? AccessToken { get; private set; }

    public Guid? ProxyId { get; private set; }

    public void SetProxy(Proxy proxy) => ProxyId = proxy.Id;

    public void UpdateToken(string token) => AccessToken = token;

    public bool IsActive() => !string.IsNullOrEmpty(AccessToken);
}