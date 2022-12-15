using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Application.Abstractions.Elements.DTOs.ParticipantReportDto;

public class ParticipantReportElementBuilder : ReportElementBaseBuilder
{
    public string? NewName;
    public ElementType? Type;
    public IEnumerable<ParticipantReportElementDto>? Children;

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
    public ParticipantReportElementBuilder WithChildren(IEnumerable<ParticipantReportElementDto> children)
    {
        Children = children;
        return this;
    }

    public ParticipantReportElementDto Build() => new(this);
}