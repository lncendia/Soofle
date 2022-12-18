using VkQ.Application.Abstractions.Elements.DTOs.Base.ElementDto;

namespace VkQ.Application.Abstractions.Elements.DTOs.Base.PublicationElementDto;

public class PublicationElementBuilder : ElementBuilder
{
    public string? LikeChatName;
    public Guid? ParticipantId;
    public bool IsAccepted;
    public bool Vip;

    public PublicationElementBuilder WithLikeChatName(string likeChatName)
    {
        LikeChatName = likeChatName;
        return this;
    }

    public PublicationElementBuilder WithParticipantId(Guid participantId)
    {
        ParticipantId = participantId;
        return this;
    }

    public PublicationElementBuilder WithAccepted(bool isAccepted)
    {
        IsAccepted = isAccepted;
        return this;
    }
    public PublicationElementBuilder WithVip(bool isVip)
    {
        Vip = isVip;
        return this;
    }
}