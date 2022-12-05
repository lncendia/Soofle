using VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.PublicationReportBaseDto;

namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.LikeReportDto;

public class LikeReportBuilder : PublicationReportBaseBuilder
{
    private LikeReportBuilder()
    {
    }

    public static LikeReportBuilder LikeReportDto() => new();

    public LikeReportBuilder WithReportElements(IEnumerable<LikeReportElementDto> elements) =>
        (LikeReportBuilder)base.WithReportElements(elements);

    public LikeReportDto Build() => new(this);
}