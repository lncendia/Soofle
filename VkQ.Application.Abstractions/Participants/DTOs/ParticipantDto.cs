using VkQ.Domain.Participants.Enums;

namespace VkQ.Application.Abstractions.Participants.DTOs;

public class ParticipantDto
{
    public ParticipantDto(Guid id, string name, string? notes, ParticipantType type, long vkId, bool vip,
        IEnumerable<ParticipantDto>? children)
    {
        Id = id;
        Name = name;
        Notes = notes;
        Type = type;
        VkId = vkId;
        Vip = vip;
        Children = children?.ToList() ?? new List<ParticipantDto>();
    }

    public Guid Id { get; }
    public string Name { get; }
    public string? Notes { get; }
    public ParticipantType Type { get; }
    public long VkId { get; }
    public bool Vip { get; }

    public List<ParticipantDto> Children { get; }
}