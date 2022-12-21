using VkQ.Domain.Reposts.BaseReport.Entities;

namespace VkQ.Domain.Reposts.PublicationReport.Entities;

public abstract class PublicationReportElement : ReportElement
{
    protected PublicationReportElement(string name, string likeChatName, long vkId, Guid participantId, bool vip,
        PublicationReportElement? parent) : base(name, vkId)
    {
        ParticipantId = participantId;
        Vip = vip;
        Parent = parent;
        LikeChatName = likeChatName;
    }

    public string LikeChatName { get; }
    public void Accept() => IsAccepted = true;
    public Guid ParticipantId { get; }
    public bool IsAccepted { get; private set; }
    public bool Vip { get; }
    protected readonly PublicationReportElement? Parent;
}