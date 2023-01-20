using Soofle.Domain.Abstractions;

namespace Soofle.Domain.Reposts.PublicationReport.Entities;

public class Publication : Entity
{
    internal Publication(long itemId, long ownerId, int id) : base(id)
    {
        ItemId = itemId;
        OwnerId = ownerId;
    }

    public long ItemId { get; }
    public long OwnerId { get; }
    public bool? IsLoaded { get; internal set; }
}