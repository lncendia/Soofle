using VkQ.Domain.Participants.Exceptions;

namespace VkQ.Domain.Participants.Entities;

public class Participant
{
    public Participant(Guid userId, string name)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public string Name { get; private set; }
    public Guid? ParentParticipantId { get; private set; }

    public void UpdateName(string name) => Name = name;

    public void SetParent(Participant parent)
    {
        if (parent.ParentParticipantId.HasValue) throw new ChildException();
        ParentParticipantId = parent.Id;
    }
}