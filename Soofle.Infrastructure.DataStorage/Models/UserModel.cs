using Soofle.Infrastructure.DataStorage.Models.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Models;

public class UserModel : IAggregateModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTimeOffset? SubscriptionDate { get; set; }
    public DateTimeOffset? ExpirationDate { get; set; }
    public VkModel? Vk { get; set; }
    public int? VkId { get; set; }
    public long? ChatId { get; set; }
}