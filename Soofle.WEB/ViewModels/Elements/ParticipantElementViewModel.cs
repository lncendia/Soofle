using Soofle.WEB.ViewModels.Elements.Base;
using Soofle.Domain.Participants.Enums;
using Soofle.Domain.Reposts.ParticipantReport.Enums;

namespace Soofle.WEB.ViewModels.Elements;

public class ParticipantElementViewModel : ElementViewModel
{
    public ParticipantElementViewModel(string name, long vkId, string? newName, ElementType? type,
        ParticipantType participantType, IEnumerable<ParticipantElementViewModel> children) : base(name, vkId)
    {
        NewName = newName;
        Type = type;
        ParticipantType = participantType;
        Children = children.ToList();
    }

    public string? NewName { get; }
    public ParticipantType ParticipantType { get; }
    public ElementType? Type { get; }
    public List<ParticipantElementViewModel> Children { get; }
}