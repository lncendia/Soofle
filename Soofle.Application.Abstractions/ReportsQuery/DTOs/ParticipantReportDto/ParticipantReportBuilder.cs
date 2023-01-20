using Soofle.Application.Abstractions.ReportsQuery.DTOs.ReportDto;

namespace Soofle.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

public class ParticipantReportBuilder : ReportBuilder
{
    public long? VkId;

    private ParticipantReportBuilder()
    {
    }

    public static ParticipantReportBuilder ParticipantReportDto() => new();

    public ParticipantReportBuilder WithVk(long vkId)
    {
        VkId = vkId;
        return this;
    }

    public ParticipantReportDto Build() => new(this);
}