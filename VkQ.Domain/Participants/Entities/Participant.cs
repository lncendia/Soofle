using VkQ.Domain.Participants.Enums;
using VkQ.Domain.Participants.Exceptions;

namespace VkQ.Domain.Participants.Entities;

public class Participant : IAggregateRoot
{
    public Participant(Guid userId, string name, long vkId, ParticipantType type)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        VkId = vkId;
        Type = type;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public string Name { get; private set; }
    public ParticipantType Type { get; }
    public long VkId { get; }
    public Guid? ParentParticipantId { get; private set; }

    public void UpdateName(string name) => Name = name;

    public void SetParent(Participant parent)
    {
        if (parent.ParentParticipantId.HasValue) throw new ChildException();
        ParentParticipantId = parent.Id;
    }
}