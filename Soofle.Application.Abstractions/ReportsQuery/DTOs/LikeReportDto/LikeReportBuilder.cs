using Soofle.Application.Abstractions.ReportsQuery.DTOs.PublicationReportDto;

namespace Soofle.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;

public class LikeReportBuilder : PublicationReportBuilder
{
    private LikeReportBuilder()
    {
    }

    public static LikeReportBuilder LikeReportDto() => new();

    public LikeReportDto Build() => new(this);
}