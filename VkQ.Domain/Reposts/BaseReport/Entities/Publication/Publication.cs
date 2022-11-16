namespace VkQ.Domain.Reposts.BaseReport.Entities.Publication;

public class Publication
{
    public Publication(int id, long itemId, long ownerId)
    {
        Id = id;
        ItemId = itemId;
        OwnerId = ownerId;
    }

    public int Id { get; }
    public long ItemId { get; }
    public long OwnerId { get; }
}