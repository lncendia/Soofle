using VkQ.Domain.Participants.Enums;

namespace VkQ.Application.Abstractions.Participants.DTOs;

public class GetParticipantDto
{
    public GetParticipantDto(Guid id, Guid? parentParticipantId, string name, string? notes, ParticipantType type, long vkId, bool vip)
    {
        Id = id;
        ParentParticipantId = parentParticipantId;
        Name = name;
        Notes = notes;
        Type = type;
        VkId = vkId;
        Vip = vip;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string? Notes { get; }
    public ParticipantType Type { get; }
    public long VkId { get; }
    public bool Vip { get; }

    public Guid? ParentParticipantId { get; }
}