using VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.PublicationReportBaseDto;

namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.LikeReportDto;

public class LikeReportElementBuilder : PublicationReportElementBaseBuilder
{
    private LikeReportElementBuilder()
    {
    }

    public static LikeReportElementBuilder LikeReportElementDto => new();

    public IEnumerable<LikeDto>? Likes;

    public LikeReportElementBuilder WithLikes(IEnumerable<LikeDto> likes)
    {
        Likes = likes;
        return this;
    }

    public LikeReportElementDto Build() => new(this);
}