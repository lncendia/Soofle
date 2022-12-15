using VkQ.Application.Abstractions.Elements.DTOs.Base.ElementBaseDto;

namespace VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

public class ParticipantReportBuilder : ReportBaseBuilder
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