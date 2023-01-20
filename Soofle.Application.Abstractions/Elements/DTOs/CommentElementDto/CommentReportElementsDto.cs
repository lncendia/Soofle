﻿using Soofle.Application.Abstractions.Elements.DTOs.PublicationElementDto;

namespace Soofle.Application.Abstractions.Elements.DTOs.CommentElementDto;

public class CommentReportElementsDto
{
    public CommentReportElementsDto(List<CommentElementDto> elements, List<PublicationDto> publications)
    {
        Elements = elements;
        Publications = publications;
    }

    public List<CommentElementDto> Elements { get; }
    public List<PublicationDto> Publications { get; }
}