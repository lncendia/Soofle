using VkQ.Domain.Participants.Enums;

namespace VkQ.WEB.ViewModels.Participants;

public class ParticipantViewModel
{
    public ParticipantViewModel(Guid id, string name, string? note, bool vip, ParticipantType type,
        IEnumerable<ParticipantViewModel>? children)
    {
        Id = id;
        Name = name;
        Note = note?[..Math.Min(note.Length, 50)];
        Vip = vip;
        Type = type;
        if (children != null) Children.AddRange(children);
    }

    public Guid Id { get; }
    public string Name { get; }
    public string? Note { get; }
    public ParticipantType Type { get; }
    public bool Vip { get; }

    public List<ParticipantViewModel> Children { get; } = new();
}