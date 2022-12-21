using VkQ.Domain.Abstractions;

namespace VkQ.Domain.Reposts.PublicationReport.Entities;

public class Publication : Entity
{
    public Publication(long itemId, long ownerId)
    {
        
        ItemId = itemId;
        OwnerId = ownerId;
    }

    public long ItemId { get; }
    public long OwnerId { get; }
}