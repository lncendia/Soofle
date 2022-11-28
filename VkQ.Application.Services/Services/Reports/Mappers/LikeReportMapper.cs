using VkQ.Application.Abstractions.DTO.Reports.LikeReportDto;
using VkQ.Application.Abstractions.Interfaces.Reports;
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
        var children = element.Children;

        if (likes.Any())
            elementBuilder.WithLikes(likes.Select(x => new LikeDto(x.PublicationId, x.IsLiked, x.IsLoaded)));
        if (children.Any())
            elementBuilder.WithChildren(children.Select(CreateElement));

        return elementBuilder.Build();
    }
}