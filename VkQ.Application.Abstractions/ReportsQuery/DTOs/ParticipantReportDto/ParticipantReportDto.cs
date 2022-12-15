namespace VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

public class ParticipantReportDto : ReportBaseDto
{
    public ParticipantReportDto(ParticipantReportBuilder builder) : base(builder)
    {
        VkId = builder.VkId ?? throw new ArgumentNullException(nameof(builder.VkId));
    }

    public long VkId { get; }
}