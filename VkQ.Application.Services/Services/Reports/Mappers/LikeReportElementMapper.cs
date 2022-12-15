using VkQ.Application.Abstractions.Elements.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.Elements.ServicesInterfaces;
using VkQ.Domain.Reposts.LikeReport.Entities;

namespace VkQ.Application.Services.Services.Reports.Mappers;

public class LikeReportElementMapper : IElementMapperUnit<LikeReportElementDto, LikeReportElement>
{
    public List<LikeReportElementDto> Map(List<LikeReportElement> elements)
    {
        var groupedElements = elements.GroupBy(x => x.Parent).ToList();

        return (from element in groupedElements.First(x => x.Key == null)
            let children = groupedElements.FirstOrDefault(x => x.Key == element)?.Select(x => Map(x, null))
            select Map(element, children)).ToList();
    }

    private static LikeReportElementDto Map(LikeReportElement element, IEnumerable<LikeReportElementDto>? children)
    {
        var elementBuilder = LikeReportElementBuilder.LikeReportElementDto;
        StaticMethods.ReportMapper.InitElementBuilder(elementBuilder, element);
        var likes = element.Likes;

        if (likes.Any())
            elementBuilder.WithLikes(likes.Select(x => new LikeDto(x.PublicationId, x.IsLiked, x.IsLoaded)));
        if (children != null) elementBuilder.WithChildren(children);
        return elementBuilder.Build();
    }
}