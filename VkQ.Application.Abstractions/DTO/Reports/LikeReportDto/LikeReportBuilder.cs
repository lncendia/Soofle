using VkQ.Application.Abstractions.DTO.Reports.Base.PublicationReportBaseDto;

namespace VkQ.Application.Abstractions.DTO.Reports.LikeReportDto;

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