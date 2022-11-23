namespace VkQ.Infrastructure.DataStorage.Models;

public class UserModel : IModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTimeOffset? SubscriptionDate { get; set; }
    public DateTimeOffset? ExpirationDate { get; set; }
    public VkModel? Vk { get; set; }
}