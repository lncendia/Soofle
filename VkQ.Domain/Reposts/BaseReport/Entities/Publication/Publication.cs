namespace VkQ.Domain.Reposts.BaseReport.Entities.Publication;

public class Publication : IEntity
{
    public Publication(long itemId, long ownerId)
    {
        Id = Guid.NewGuid();
        ItemId = itemId;
        OwnerId = ownerId;
    }

    public Guid Id { get; }
    public long ItemId { get; }
    public long OwnerId { get; }
}