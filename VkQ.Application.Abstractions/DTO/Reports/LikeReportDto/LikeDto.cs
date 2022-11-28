﻿namespace VkQ.Application.Abstractions.DTO.Reports.LikeReportDto;

public class LikeDto
{
    public LikeDto(Guid publicationId, bool isLiked, bool isLoaded)
    {
        PublicationId = publicationId;
        IsLiked = isLiked;
        IsLoaded = isLoaded;
    }

    public Guid PublicationId { get; }
    public bool IsLiked { get; }
    public bool IsLoaded { get; }
}