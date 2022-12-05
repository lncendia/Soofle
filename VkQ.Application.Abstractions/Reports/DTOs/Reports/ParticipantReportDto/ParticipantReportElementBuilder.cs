using VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.ReportBaseDto;
using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.ParticipantReportDto;

public class ParticipantReportElementBuilder : ReportElementBaseBuilder
{
    public string? NewName;
    public ElementType? Type;

    private ParticipantReportElementBuilder()
    {
    }

    public static ParticipantReportElementBuilder ParticipantReportElementDto() => new();

    public ParticipantReportElementBuilder WithType(ElementType type, string? newName = null)
    {
        NewName = newName;
        Type = type;
        return this;
    }

    public ParticipantReportElementDto Build() => new(this);
}