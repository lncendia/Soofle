using VkQ.Domain.Reposts.ParticipantReport.Enums;
using VkQ.WEB.ViewModels.Elements.Base;

namespace VkQ.WEB.ViewModels.Elements;

public class ParticipantElementViewModel : ElementViewModel
{
    public ParticipantElementViewModel(string name, long vkId, string? newName, ElementType? type) : base(name, vkId)
    {
        NewName = newName;
        Type = type;
    }

    public string? NewName { get; }
    public ElementType? Type { get; }
}