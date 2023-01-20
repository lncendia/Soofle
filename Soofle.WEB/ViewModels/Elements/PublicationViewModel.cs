namespace Soofle.WEB.ViewModels.Elements;

public class PublicationViewModel
{
    public PublicationViewModel(int id, long itemId, long ownerId, bool isLoaded)
    {
        Id = id;
        ItemId = itemId;
        OwnerId = ownerId;
        IsLoaded = isLoaded;
    }

    public int Id { get; }
    public long ItemId { get; }
    public long OwnerId { get; }
    public bool IsLoaded { get; }
}