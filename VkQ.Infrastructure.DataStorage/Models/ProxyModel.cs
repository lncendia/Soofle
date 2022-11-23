namespace VkQ.Infrastructure.DataStorage.Models;

public class ProxyModel : IModel
{
    public Guid Id { get; set; }
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}