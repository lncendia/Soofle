using VkQ.Domain.Participants.Exceptions;

namespace VkQ.Domain.Participants.Entities;

public class Participant
{
    public Participant(Guid userId, string name, long vkId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        VkId = vkId;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public string Name { get; private set; }
    public long VkId { get; }
    public Guid? ParentParticipantId { get; private set; }

    public void UpdateName(string name) => Name = name;

    public void SetParent(Participant parent)
    {
        if (parent.ParentParticipantId.HasValue) throw new ChildException();
        ParentParticipantId = parent.Id;
    }
}