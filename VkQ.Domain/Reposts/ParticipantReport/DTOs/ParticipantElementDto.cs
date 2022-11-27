using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Domain.Reposts.ParticipantReport.DTOs;

public class AddParticipantDto
{
    public AddParticipantDto(string name, string? oldName, long vkId, ElementType type,
        List<AddParticipantDto>? children)
    {
        Name = name;
        VkId = vkId;
        Type = type;
        Children = children;
        OldName = oldName;
    }

    public string Name { get; }
    public string? OldName { get; }
    public long VkId { get; }
    public ElementType Type { get; }

    public List<AddParticipantDto>? Children { get; }
}