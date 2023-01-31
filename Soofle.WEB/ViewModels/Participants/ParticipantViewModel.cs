using Soofle.Domain.Participants.Enums;

namespace Soofle.WEB.ViewModels.Participants;

public class ParticipantViewModel
{
    public ParticipantViewModel(Guid id, string name, long vkId, string? note, bool vip, ParticipantType type,
        IEnumerable<ParticipantViewModel>? children)
    {
        Id = id;
        Name = name;
        Note = note?[..Math.Min(note.Length, 50)];
        Vip = vip;
        Type = type;
        VkId = vkId;
        if (children != null) Children.AddRange(children);
    }

    public Guid Id { get; }
    public long VkId { get; }
    public string Name { get; }
    public string? Note { get; }
    public ParticipantType Type { get; }
    public bool Vip { get; }

    public List<ParticipantViewModel> Children { get; } = new();
}