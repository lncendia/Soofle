﻿namespace VkQ.Domain.Reposts.BaseReport.DTOs;

public class PublicationDto
{
    public PublicationDto(long itemId, long ownerId)
    {
        ItemId = itemId;
        OwnerId = ownerId;
    }

    public long ItemId { get; }
    public long OwnerId { get; }
}