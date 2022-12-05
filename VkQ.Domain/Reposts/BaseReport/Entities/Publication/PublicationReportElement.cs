using VkQ.Domain.Reposts.BaseReport.Entities.Base;

namespace VkQ.Domain.Reposts.BaseReport.Entities.Publication;

public abstract class PublicationReportElement : ReportElement
{
    protected PublicationReportElement(string name, string likeChatName, long vkId, Guid participantId, PublicationReportElement? parent) : base(name, vkId, parent)
    {
        ParticipantId = participantId;
        LikeChatName = likeChatName;
    }

    public string LikeChatName { get; }
    public void Accept() => IsAccepted = true;
    public Guid ParticipantId { get; }
    public bool IsAccepted { get; private set; }
}