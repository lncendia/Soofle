using VkQ.Application.Abstractions.Reports.DTOs.Reports.LikeReportDto;
using VkQ.Application.Abstractions.Reports.ServicesInterfaces;
using VkQ.Application.Services.Services.Reports.Mappers.StaticMethods;
using VkQ.Domain.Reposts.LikeReport.Entities;

namespace VkQ.Application.Services.Services.Reports.Mappers;

public class LikeReportMapper : IReportMapper<LikeReportDto, LikeReport>
{
    public LikeReportDto Map(LikeReport report)
    {
        var builder = LikeReportBuilder.LikeReportDto();
        ReportMapper.InitReportBuilder(builder, report);
        var elements = report.Elements;
        if (elements.Any()) builder.WithReportElements(elements.Select(CreateElement));
        return builder.Build();
    }

    private static LikeReportElementDto CreateElement(LikeReportElement element)
    {
        var elementBuilder = LikeReportElementBuilder.LikeReportElementDto;
        ReportMapper.InitElementBuilder(elementBuilder, element);
        var likes = element.Likes;

        if (likes.Any())
            elementBuilder.WithLikes(likes.Select(x => new LikeDto(x.PublicationId, x.IsLiked, x.IsLoaded)));
        if (element.Parent != null) elementBuilder.WithParent(element.Parent.Id);

        return elementBuilder.Build();
    }
}