using VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.ReportBaseDto;

namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.ParticipantReportDto;

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

    public ParticipantReportBuilder WithReportElements(IEnumerable<ParticipantReportElementDto> reportElements) =>
        (ParticipantReportBuilder)base.WithReportElements(reportElements);

    public ParticipantReportDto Build() => new(this);
}