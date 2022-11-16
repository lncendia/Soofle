using VkQ.Domain.Proxies.Entities;

namespace VkQ.Domain.Users.Entities;

public class Vk
{
    public Vk(int id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }

    public int Id { get; }
    
    public string Username { get; }
    public string Password { get; }
    public string? AccessToken { get; private set; }

    public Guid? ProxyId { get; private set; }

    public void SetProxy(Proxy proxy) => ProxyId = proxy.Id;
    
    public void UpdateToken(string token) => AccessToken = token;

    public bool IsActive() => !string.IsNullOrEmpty(AccessToken);
}