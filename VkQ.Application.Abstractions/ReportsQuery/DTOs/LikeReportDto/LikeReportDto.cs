using VkQ.Application.Abstractions.ReportsQuery.DTOs.Base.PublicationReportBaseDto;

namespace VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;

public class LikeReportDto : PublicationReportBaseDto
{
    public LikeReportDto(LikeReportBuilder builder) : base(builder)
    {
    }
}