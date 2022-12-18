using VkQ.Application.Abstractions.ReportsQuery.DTOs.Base.ReportBaseDto;

namespace VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

public class ParticipantReportDto : ReportBaseDto
{
    public ParticipantReportDto(ParticipantReportBuilder builder) : base(builder)
    {
        VkId = builder.VkId ?? throw new ArgumentException("builder not formed", nameof(builder));
    }

    public long VkId { get; }
}