using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Application.Abstractions.Elements.DTOs;

public class ParticipantElementSearchQuery
{
    public ParticipantElementSearchQuery(int page, string? name, string? newName, ElementType? elementType, bool? hasChildren)
    {
        Page = page;
        Name = name;
        NewName = newName;
        ElementType = elementType;
        HasChildren = hasChildren;
    }

    public int Page { get; }
    public string? Name { get; }
    public string? NewName { get; }
    public ElementType? ElementType { get; }
    public bool? HasChildren { get; }
}