using VkQ.Application.Abstractions.DTO.Reports.Base.ReportBaseDto;

namespace VkQ.Application.Abstractions.DTO.Reports.ParticipantReportDto;

public class ParticipantReportBuilder : ReportBaseBuilder
{
    public long? VkId;

    public ParticipantReportBuilder WithVk(long vkId)
    {
        VkId = vkId;
        return this;
    }

    public ParticipantReportDto Build() => new(this);
}