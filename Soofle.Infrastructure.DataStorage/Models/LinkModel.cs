using Soofle.Infrastructure.DataStorage.Models.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Models;

public class LinkModel : IAggregateModel
{
    public Guid Id { get; set; }
    public Guid User1Id { get; set; }
    public Guid User2Id { get; set; }
    public UserModel User1 { get; set; } = null!;
    public UserModel User2 { get; set; } = null!;
    public bool IsAccepted { get; set; }
}