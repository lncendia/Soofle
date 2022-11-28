using VkQ.Application.Abstractions.DTO.Reports.Base.ReportBaseDto;

namespace VkQ.Application.Abstractions.DTO.Reports.ParticipantReportDto;

public class ParticipantReportDto : ReportBaseDto
{
    public ParticipantReportDto(ParticipantReportBuilder builder) : base(builder)
    {
        VkId = builder.VkId ?? throw new ArgumentNullException(nameof(builder.VkId));
    }

    public long VkId { get; }
}