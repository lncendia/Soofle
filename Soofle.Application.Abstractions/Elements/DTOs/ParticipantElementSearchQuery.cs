using Soofle.Domain.Participants.Enums;
using Soofle.Domain.Reposts.ParticipantReport.Enums;

namespace Soofle.Application.Abstractions.Elements.DTOs;

public class ParticipantElementSearchQuery
{
    public ParticipantElementSearchQuery(int page, string? name, ElementType? elementType, bool? hasChildren, ParticipantType? participantType)
    {
        Page = page;
        NameNormalized = name?.ToUpper();
        ElementType = elementType;
        HasChildren = hasChildren;
        ParticipantType = participantType;
    }

    public int Page { get; }
    public string? NameNormalized { get; }
    public ParticipantType? ParticipantType { get; }
    public ElementType? ElementType { get; }
    public bool? HasChildren { get; }
}