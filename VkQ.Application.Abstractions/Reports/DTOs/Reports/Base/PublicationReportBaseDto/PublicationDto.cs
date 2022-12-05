namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.PublicationReportBaseDto;

public class PublicationDto
{
    public PublicationDto(Guid id, long itemId, long ownerId)
    {
        Id = id;
        ItemId = itemId;
        OwnerId = ownerId;
    }

    public Guid Id { get; }
    public long ItemId { get; }
    public long OwnerId { get; }
}