using Soofle.Domain.Participants.Enums;
using Soofle.Domain.Reposts.ParticipantReport.Enums;

namespace Soofle.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

public class ParticipantElementDto : ElementDto.ElementDto
{
    public ParticipantElementDto(ParticipantElementBuilder builder) : base(builder)
    {
        Type = builder.Type;
        NewName = builder.NewName;
        ParticipantType = builder.ParticipantType;
        if (builder.Children != null) Children.AddRange(builder.Children);
    }

    public string? NewName { get; }
    public ElementType? Type { get; }
    public ParticipantType ParticipantType { get; }
    public List<ParticipantElementDto> Children { get; } = new();
}