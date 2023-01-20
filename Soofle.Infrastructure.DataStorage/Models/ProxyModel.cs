using System.ComponentModel.DataAnnotations.Schema;
using Soofle.Infrastructure.DataStorage.Models.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Models;

public class ProxyModel : IAggregateModel
{
    public Guid Id { get; set; }
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    [Column(TypeName = "varchar(60)")] public string Login { get; set; } = null!;
    [Column(TypeName = "varchar(60)")] public string Password { get; set; } = null!;
}