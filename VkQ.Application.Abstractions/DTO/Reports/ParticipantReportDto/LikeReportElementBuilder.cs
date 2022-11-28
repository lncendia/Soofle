using VkQ.Application.Abstractions.DTO.Reports.Base.PublicationReportBaseDto;
using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Application.Abstractions.DTO.Reports.ParticipantReportDto;

public class ParticipantReportElementBuilder : PublicationReportElementBaseBuilder
{
    public string? OldName;
    public ElementType? Type;

    public ParticipantReportElementBuilder WithOldName(string oldName)
    {
        OldName = oldName;
        return this;
    }

    public ParticipantReportElementBuilder WithType(ElementType type)
    {
        Type = type;
        return this;
    }

    public ParticipantReportElementBuilder WithChildren(IEnumerable<ParticipantReportElementDto> children) =>
        (ParticipantReportElementBuilder)base.WithChildren(children);

    public ParticipantReportElementDto Build() => new(this);
}