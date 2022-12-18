namespace VkQ.WEB.ViewModels.Reports;

public class PublicationViewModel
{
    public PublicationViewModel(Guid id, long itemId, long ownerId)
    {
        Id = id;
        ItemId = itemId;
        OwnerId = ownerId;
    }

    public Guid Id { get; }
    public long ItemId { get; }
    public long OwnerId { get; }
}