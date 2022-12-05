using VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.ReportBaseDto;

namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.ParticipantReportDto;

public class ParticipantReportDto : ReportBaseDto
{
    public ParticipantReportDto(ParticipantReportBuilder builder) : base(builder)
    {
        VkId = builder.VkId ?? throw new ArgumentNullException(nameof(builder.VkId));
    }

    public long VkId { get; }
}