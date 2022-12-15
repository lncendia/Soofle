using VkQ.Application.Abstractions.ReportsQuery.DTOs.Base.PublicationReportBaseDto;

namespace VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;

public class LikeReportBuilder : PublicationReportBaseBuilder
{
    private LikeReportBuilder()
    {
    }

    public static LikeReportBuilder LikeReportDto() => new();

    public LikeReportDto Build() => new(this);
}