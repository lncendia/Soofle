using VkQ.Domain.Reposts.BaseReport.Entities.Base;
using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Domain.Reposts.ParticipantReport.Entities;

public class ParticipantReportElement : ReportElement
{
    public ParticipantReportElement(string name, string? oldName, long vkId, ElementType type,
        IEnumerable<ParticipantReportElement>? children) : base(name, vkId, children)
    {
        if (type == ElementType.Rename && string.IsNullOrEmpty(oldName))
            throw new ArgumentException("OldName is required for rename element");
        Type = type;
        OldName = oldName;
    }

    public new List<ParticipantReportElement> Children => base.Children.Cast<ParticipantReportElement>().ToList();
    public string? OldName { get; }
    public ElementType Type { get; }
}