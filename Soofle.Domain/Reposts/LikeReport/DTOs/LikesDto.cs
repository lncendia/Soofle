﻿namespace Soofle.Domain.Reposts.LikeReport.DTOs;

public class LikesDto
{
    public LikesDto(long publicationId, long ownerId, IReadOnlyCollection<long> likes)
    {
        PublicationId = publicationId;
        Likes = likes;
        SuccessLoaded = true;
        OwnerId = ownerId;
    }
    public LikesDto(long publicationId, long ownerId)
    {
        PublicationId = publicationId;
        OwnerId = ownerId;
        SuccessLoaded = false;
    }

    public long OwnerId { get; }
    public long PublicationId { get; }
    public IReadOnlyCollection<long>? Likes { get; }
    public bool SuccessLoaded { get; }
}