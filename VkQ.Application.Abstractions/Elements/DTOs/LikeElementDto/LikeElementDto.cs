using VkQ.Application.Abstractions.Elements.DTOs.Base.PublicationElementDto;

namespace VkQ.Application.Abstractions.Elements.DTOs.LikeElementDto;

public class LikeElementDto : PublicationElementDto
{
    public LikeElementDto(LikeElementBuilder builder) : base(builder)
    {
        if (builder.Likes != null) Likes.AddRange(builder.Likes);
    }

    public List<LikeDto> Likes { get; } = new();
    public List<LikeElementDto> Children { get; } = new(); //todo:fg
}