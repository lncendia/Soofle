using System.ComponentModel.DataAnnotations.Schema;

namespace VkQ.Infrastructure.DataStorage.Models;

public class VkModel : IModel
{
    public Guid Id { get; set; }
    [Column(TypeName = "varchar(60)")] public string Username { get; set; } = null!;
    [Column(TypeName = "varchar(60)")] public string Password { get; set; } = null!;
    public string? AccessToken { get; set; }

    public Guid? ProxyId { get; set; }
    public ProxyModel? ProxyModel { get; set; }

    public UserModel UserModel { get; set; } = null!;
}