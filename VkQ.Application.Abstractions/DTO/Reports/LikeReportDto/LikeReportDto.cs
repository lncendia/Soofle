using VkQ.Application.Abstractions.DTO.Reports.Base.PublicationReportBaseDto;

namespace VkQ.Application.Abstractions.DTO.Reports.LikeReportDto;

public class LikeReportDto : PublicationReportBaseDto
{
    public LikeReportDto(LikeReportBuilder builder) : base(builder)
    {
    }

    public List<LikeReportElementDto> LikeReportElements => ReportElementsList.Cast<LikeReportElementDto>().ToList();
}