using VkQ.Application.Abstractions.Elements.DTOs.Base.PublicationElementDto;

namespace VkQ.Application.Abstractions.Elements.DTOs.LikeElementDto;

public class LikeElementBuilder : PublicationElementBuilder
{
    public IEnumerable<LikeElementDto>? Children;
    private LikeElementBuilder()
    {
    }

    public static LikeElementBuilder LikeReportElementDto => new();

    public IEnumerable<LikeDto>? Likes;

    public LikeElementBuilder WithLikes(IEnumerable<LikeDto> likes)
    {
        Likes = likes;
        return this;
    }
    public LikeElementBuilder WithChildren(IEnumerable<LikeElementDto> children)
    {
        Children = children;
        return this;
    }

    public LikeElementDto Build() => new(this);
}