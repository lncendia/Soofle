using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Application.Abstractions.Elements.DTOs;

public class ParticipantElementSearchQuery
{
    public ParticipantElementSearchQuery(int page, string? name, ElementType? elementType, bool? hasChildren)
    {
        Page = page;
        Name = name;
        ElementType = elementType;
        HasChildren = hasChildren;
    }

    public int Page { get; }
    public string? Name { get; }
    public ElementType? ElementType { get; }
    public bool? HasChildren { get; }
}