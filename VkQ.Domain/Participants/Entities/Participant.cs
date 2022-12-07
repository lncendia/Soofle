using VkQ.Domain.Abstractions;
using VkQ.Domain.Participants.Enums;
using VkQ.Domain.Participants.Exceptions;

namespace VkQ.Domain.Participants.Entities;

public class Participant : AggregateRoot
{
    public Participant(Guid userId, string name, long vkId, ParticipantType type)
    {
        UserId = userId;
        Name = name;
        VkId = vkId;
        Type = type;
    }


    public Guid UserId { get; }
    public string Name { get; private set; }
    public string? Notes { get; private set; }
    public ParticipantType Type { get; }
    public long VkId { get; }
    public Guid? ParentParticipantId { get; private set; }
    public bool Vip { get; private set; }

    public void UpdateName(string name) => Name = name;

    /// <exception cref="ChildException"></exception>
    public void SetParent(Participant parent)
    {
        if (parent.ParentParticipantId.HasValue) throw new ChildException();
        ParentParticipantId = parent.Id;
    }

    public void SetNotes(string notes)
    {
        if (notes.Length > 500 || string.IsNullOrEmpty(notes))
            throw new ArgumentException("Notes length must be less than 500");
        Notes = notes;
    }

    public void SetVip(bool vip) => Vip = vip;
}