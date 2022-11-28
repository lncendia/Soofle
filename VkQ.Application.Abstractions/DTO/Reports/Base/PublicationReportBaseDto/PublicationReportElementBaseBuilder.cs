using VkQ.Application.Abstractions.DTO.Reports.Base.ReportBaseDto;

namespace VkQ.Application.Abstractions.DTO.Reports.Base.PublicationReportBaseDto;

public class PublicationReportElementBaseBuilder : ReportElementBaseBuilder
{
    public string? LikeChatName;
    public Guid? ParticipantId;
    public bool IsAccepted;

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

    protected PublicationReportElementBaseBuilder WithChildren(IEnumerable<PublicationReportElementBaseDto> children) =>
        (PublicationReportElementBaseBuilder)base.WithChildren(children);
}