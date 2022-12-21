using VkQ.Application.Abstractions.Elements.DTOs.ElementDto;
using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

public class ParticipantElementBuilder : ElementBuilder
{
    public string? NewName;
    public ElementType? Type;
    public IEnumerable<ParticipantElementDto>? Children;

    private ParticipantElementBuilder()
    {
    }

    public static ParticipantElementBuilder ParticipantReportElementDto() => new();

    public ParticipantElementBuilder WithType(ElementType type, string? newName = null)
    {
        NewName = newName;
        Type = type;
        return this;
    }
    public ParticipantElementBuilder WithChildren(IEnumerable<ParticipantElementDto> children)
    {
        Children = children;
        return this;
    }

    public ParticipantElementDto Build() => new(this);
}