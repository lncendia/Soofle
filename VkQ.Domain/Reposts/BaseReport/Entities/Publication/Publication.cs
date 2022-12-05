using VkQ.Domain.Abstractions;

namespace VkQ.Domain.Reposts.BaseReport.Entities.Publication;

public class Publication : Entity
{
    public Publication(long itemId, long ownerId)
    {
        
        ItemId = itemId;
        OwnerId = ownerId;
    }

    public Guid Id { get; }
    public long ItemId { get; }
    public long OwnerId { get; }
}