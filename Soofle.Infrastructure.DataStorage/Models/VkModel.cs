using System.ComponentModel.DataAnnotations.Schema;
using Soofle.Infrastructure.DataStorage.Models.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Models;

public class VkModel : IEntityModel
{
    public int Id { get; set; }

    public int EntityId { get; set; }
    [Column(TypeName = "nvarchar(100)")] public string Username { get; set; } = null!;
    [Column(TypeName = "nvarchar(100)")] public string Password { get; set; } = null!;
    [Column(TypeName = "nvarchar(500)")] public string? AccessToken { get; set; }
    public Guid? ProxyId { get; set; }
    public ProxyModel? Proxy { get; set; }
    public UserModel User { get; set; } = null!;
}