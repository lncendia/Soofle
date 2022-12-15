using VkQ.Application.Abstractions.Elements.DTOs.Base.PublicationElementBaseDto;

namespace VkQ.Application.Abstractions.Elements.DTOs.LikeReportDto;

public class LikeReportElementBuilder : PublicationReportElementBaseBuilder
{
    public IEnumerable<LikeReportElementDto>? Children;
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
    public LikeReportElementBuilder WithChildren(IEnumerable<LikeReportElementDto> children)
    {
        Children = children;
        return this;
    }

    public LikeReportElementDto Build() => new(this);
}