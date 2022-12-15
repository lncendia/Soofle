namespace VkQ.Application.Abstractions.Elements.DTOs;

public class PublicationElementSearchQuery
{
    public PublicationElementSearchQuery(int page, string? name, bool? succeeded, string? likeChatName, bool? hasChildren, bool? vip)
    {
        Page = page;
        Name = name;
        Succeeded = succeeded;
        LikeChatName = likeChatName;
        HasChildren = hasChildren;
        Vip = vip;
    }

    public int Page { get; }
    public string? Name { get; }
    public bool? Succeeded { get; }
    public string? LikeChatName { get; }
    public bool? HasChildren { get; }
    public bool? Vip { get; }
}