using Soofle.Domain.Participants.Enums;

namespace Soofle.Application.Abstractions.Participants.DTOs;

public class SearchQuery
{
    public SearchQuery(int page, string? name = null, ParticipantType? type = null, bool? vip = null,
        bool? hasChildren = null)
    {
        Page = page;
        NameNormalized = name?.ToUpper();
        Type = type;
        Vip = vip;
        HasChildren = hasChildren;
    }

    public int Page { get; }
    public string? NameNormalized { get; }
    public ParticipantType? Type { get; }
    public bool? Vip { get; }
    public bool? HasChildren { get; }
}