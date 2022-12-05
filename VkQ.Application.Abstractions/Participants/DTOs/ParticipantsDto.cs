using VkQ.Domain.Participants.Enums;

namespace VkQ.Application.Abstractions.Participants.DTOs;

public class ParticipantsDto
{
    public ParticipantsDto(int count, IEnumerable<ParticipantDto> participants)
    {
        Count = count;
        Participants.AddRange(participants);
    }

    public int Count { get; }
    public List<ParticipantDto> Participants { get; } = new();
}

public class ParticipantDto
{
    public ParticipantDto(Guid id, string name, string? notes, ParticipantType type, long vkId,
        Guid? parentParticipantId, bool vip)
    {
        Id = id;
        Name = name;
        Notes = notes;
        Type = type;
        VkId = vkId;
        ParentParticipantId = parentParticipantId;
        Vip = vip;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string? Notes { get; }
    public ParticipantType Type { get; }
    public long VkId { get; }
    public Guid? ParentParticipantId { get; }
    public bool Vip { get; }
}