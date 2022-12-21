using VkQ.Domain.Reposts.ParticipantReport.Enums;
using VkQ.WEB.ViewModels.Elements.Base;

namespace VkQ.WEB.ViewModels.Elements;

public class ParticipantElementViewModel : ElementViewModel
{
    public ParticipantElementViewModel(string name, long vkId, string? newName, ElementType? type, IEnumerable<ParticipantElementViewModel> children) : base(name, vkId)
    {
        NewName = newName;
        Type = type;
        Children = children.ToList();
    }

    public string? NewName { get; }
    public ElementType? Type { get; }
    public List<ParticipantElementViewModel> Children { get; }
}