using VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.PublicationReportBaseDto;

namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.LikeReportDto;

public class LikeReportDto : PublicationReportBaseDto
{
    public LikeReportDto(LikeReportBuilder builder) : base(builder)
    {
    }

    public List<LikeReportElementDto> LikeReportElements => ReportElementsList.Cast<LikeReportElementDto>().ToList();
}