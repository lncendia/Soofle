using Soofle.Domain.Participants.Enums;
using Soofle.Domain.Reposts.BaseReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Enums;

namespace Soofle.Domain.Reposts.ParticipantReport.Entities;

public class ParticipantReportElement : ReportElement
{
    internal ParticipantReportElement(string name, long vkId, Guid? participantId, ParticipantType participantType,
        ParticipantReportElement? parent, int id) : base(name, vkId, id)
    {
        ParticipantType = participantType;
        Parent = parent;
        ParticipantId = participantId;
    }

    public ParticipantReportElement? Parent { get; }
    public Guid? ParticipantId { get; }
    public string? NewName { get; private set; }
    public ElementType? Type { get; private set; }
    public ParticipantType ParticipantType { get; }

    internal void SetType(ElementType type, string? newName = null)
    {
        if (type != ElementType.New && !ParticipantId.HasValue)
            throw new InvalidOperationException("ParticipantId is null");

        if (type == ElementType.Rename && string.IsNullOrEmpty(newName))
            throw new ArgumentException("New name is required for rename element");

        Type = type;
        NewName = newName;
    }
}