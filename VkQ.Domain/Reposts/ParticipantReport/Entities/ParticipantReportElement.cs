﻿using VkQ.Domain.Participants.Enums;
using VkQ.Domain.Reposts.BaseReport.Entities.Base;
using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Domain.Reposts.ParticipantReport.Entities;

public class ParticipantReportElement : ReportElement
{
    internal ParticipantReportElement(string name, long vkId, Guid? participantId, ParticipantType participantType,
        ParticipantReportElement? parent) : base(name, vkId, parent)
    {
        ParticipantType = participantType;
        ParticipantId = participantId;
    }

    public new ParticipantReportElement? Parent => base.Parent as ParticipantReportElement;
    public Guid? ParticipantId { get; }
    public string? NewName { get; private set; }
    public ElementType? Type { get; private set; }
    public ParticipantType ParticipantType { get; }

    internal void SetType(ElementType type, string? newName = null)
    {
        if (type != ElementType.New && !ParticipantId.HasValue)
            throw new InvalidOperationException("ParticipantId is null");

        if (type == ElementType.Rename && string.IsNullOrEmpty(newName))
            throw new ArgumentException("OldName is required for rename element");

        Type = type;
        NewName = newName;
    }
}