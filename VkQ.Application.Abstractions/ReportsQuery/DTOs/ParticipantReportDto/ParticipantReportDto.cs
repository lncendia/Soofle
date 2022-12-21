using VkQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;

namespace VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

public class ParticipantReportDto : ReportDto.ReportDto
{
    public ParticipantReportDto(ParticipantReportBuilder builder) : base(builder)
    {
        VkId = builder.VkId ?? throw new ArgumentException("builder not formed", nameof(builder));
    }

    public long VkId { get; }
}