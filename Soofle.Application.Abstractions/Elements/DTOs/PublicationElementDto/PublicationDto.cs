namespace Soofle.Application.Abstractions.Elements.DTOs.PublicationElementDto;

public class PublicationDto
{
    public PublicationDto(int id, long itemId, long ownerId, bool isLoaded)
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