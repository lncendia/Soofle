using VkQ.Application.Abstractions.Elements.DTOs.Base.ElementDto;
using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

public class ParticipantElementDto : ElementDto
{
    public ParticipantElementDto(ParticipantElementBuilder builder) : base(builder)
    {
        Type = builder.Type;
        NewName = builder.NewName;
    }

    public string? NewName { get; }
    public ElementType? Type { get; }
}