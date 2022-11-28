using VkQ.Application.Abstractions.DTO.Reports.Base.PublicationReportBaseDto;

namespace VkQ.Application.Abstractions.DTO.Reports.LikeReportDto;

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

    public LikeReportElementBuilder WithChildren(IEnumerable<LikeReportElementDto> children) =>
        (LikeReportElementBuilder)base.WithChildren(children);

    public LikeReportElementDto Build() => new(this);
}