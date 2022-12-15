using VkQ.Application.Abstractions.Elements.DTOs.Base.PublicationElementBaseDto;

namespace VkQ.Application.Abstractions.Elements.DTOs.LikeReportDto;

public class LikeReportElementDto : PublicationReportElementBaseDto
{
    public LikeReportElementDto(LikeReportElementBuilder builder) : base(builder)
    {
        if (builder.Likes != null) Likes.AddRange(builder.Likes);
    }

    public List<LikeDto> Likes { get; } = new();
}