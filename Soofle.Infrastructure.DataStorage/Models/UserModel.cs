using System.ComponentModel.DataAnnotations.Schema;
using Soofle.Infrastructure.DataStorage.Models.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Models;

public class UserModel : IAggregateModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTimeOffset? SubscriptionDate { get; set; }
    public DateTimeOffset? ExpirationDate { get; set; }
    [Column(TypeName = "nvarchar(100)")] public string? VkName { get; set; }

    [Column(TypeName = "nvarchar(500)")] public string? AccessToken { get; set; }

    public Guid? ProxyId { get; set; }
    public ProxyModel? Proxy { get; set; }
    public long? Target { get; set; }
    public DateTimeOffset? TargetSetTime { get; set; }
}