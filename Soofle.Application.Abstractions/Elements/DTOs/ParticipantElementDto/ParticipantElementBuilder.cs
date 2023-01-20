using Soofle.Application.Abstractions.Elements.DTOs.ElementDto;
using Soofle.Domain.Participants.Enums;
using Soofle.Domain.Reposts.ParticipantReport.Enums;

namespace Soofle.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

public class ParticipantElementBuilder : ElementBuilder
{
    public string? NewName { get; private set; }
    public ElementType? Type { get; private set; }
    public ParticipantType ParticipantType { get; private set; }
    public IEnumerable<ParticipantElementDto>? Children { get; private set; }

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
    
    public ParticipantElementBuilder WithParticipantType(ParticipantType type)
    {
        ParticipantType = type;
        return this;
    }

    public ParticipantElementDto Build() => new(this);
}