using VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.PublicationReportBaseDto;

namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.LikeReportDto;

public class LikeReportElementDto : PublicationReportElementBaseDto
{
    public LikeReportElementDto(LikeReportElementBuilder builder) : base(builder)
    {
        if (builder.Likes != null) Likes.AddRange(builder.Likes);
    }

    public List<LikeDto> Likes { get; } = new();
}