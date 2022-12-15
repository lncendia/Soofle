namespace VkQ.Application.Abstractions.Elements.DTOs.Base.PublicationElementBaseDto;

public class PublicationReportElementBaseBuilder : ReportElementBaseBuilder
{
    public string? LikeChatName;
    public Guid? ParticipantId;
    public bool IsAccepted;
    public bool Vip;

    public PublicationReportElementBaseBuilder WithLikeChatName(string likeChatName)
    {
        LikeChatName = likeChatName;
        return this;
    }

    public PublicationReportElementBaseBuilder WithParticipantId(Guid participantId)
    {
        ParticipantId = participantId;
        return this;
    }

    public PublicationReportElementBaseBuilder WithAccepted(bool isAccepted)
    {
        IsAccepted = isAccepted;
        return this;
    }
    public PublicationReportElementBaseBuilder WithVip(bool isVip)
    {
        Vip = isVip;
        return this;
    }
}