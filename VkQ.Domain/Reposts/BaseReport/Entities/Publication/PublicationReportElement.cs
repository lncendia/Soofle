using VkQ.Domain.Reposts.BaseReport.Entities.Base;

namespace VkQ.Domain.Reposts.BaseReport.Entities.Publication;

public abstract class PublicationReportElement : ReportElement
{
    protected PublicationReportElement(string name, string likeChatName, long vkId, Guid participantId,
        IEnumerable<PublicationReportElement>? children) : base(name, vkId, children)
    {
        ParticipantId = participantId;
        LikeChatName = likeChatName;
    }
    public string LikeChatName { get; }
    public void Accept() => IsAccepted = true;
    public Guid ParticipantId { get; }
    public bool IsAccepted { get; private set; }
}