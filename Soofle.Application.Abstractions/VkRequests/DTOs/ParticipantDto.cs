using Soofle.Domain.Participants.Enums;

namespace Soofle.Application.Abstractions.VkRequests.DTOs;

public class ParticipantDto
{
    public ParticipantDto(long vkId, string name, ParticipantType type)
    {
        VkId = vkId;
        Name = name;
        Type = type;
    }

    public long VkId { get; }
    public string Name { get; }
    public ParticipantType Type { get; }
}