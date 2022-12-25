using VkQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using VkQ.Application.Abstractions.Elements.ServicesInterfaces;
using VkQ.Domain.Reposts.LikeReport.Entities;

namespace VkQ.Application.Services.Elements.Mappers;

public class LikeReportElementMapper : IElementMapperUnit<LikeElementDto, LikeReportElement>
{
    public List<LikeElementDto> Map(List<LikeReportElement> elements)
    {
        var groupedElements = elements.GroupBy(x => x.Parent).ToList();
        if (!groupedElements.Any()) return new List<LikeElementDto>();
        return (from element in groupedElements.First(x => x.Key == null)
            let children = groupedElements.FirstOrDefault(x => x.Key == element)?.Select(x => Map(x, null))
            select Map(element, children)).ToList();
    }

    private static LikeElementDto Map(LikeReportElement element, IEnumerable<LikeElementDto>? children)
    {
        var elementBuilder = LikeElementBuilder.LikeReportElementDto;
        StaticMethods.ElementMapper.InitElementBuilder(elementBuilder, element);
        var likes = element.Likes;

        if (likes.Any())
            elementBuilder.WithLikes(likes.Select(x => new LikeDto(x.PublicationId, x.IsLiked, x.IsLoaded)));
        if (children != null) elementBuilder.WithChildren(children);
        return elementBuilder.Build();
    }
}