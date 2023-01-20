using Soofle.Domain.Reposts.BaseReport.Entities;

namespace Soofle.Domain.Reposts.PublicationReport.Entities;

public abstract class PublicationReportElement : ReportElement
{
    protected internal PublicationReportElement(string name, string likeChatName, long vkId, Guid participantId,
        bool vip, string? note, PublicationReportElement? parent, int id) : base(name, vkId, id)
    {
        ParticipantId = participantId;
        Vip = vip;
        Parent = parent;
        LikeChatName = likeChatName;
        if (note != null) Note = note[..Math.Min(note.Length, 10)] + "...";
    }

    public string LikeChatName { get; }
    public void Accept() => IsAccepted = true;
    public Guid ParticipantId { get; }
    public bool IsAccepted { get; private set; }
    public bool Vip { get; }
    public string? Note { get; }
    protected readonly PublicationReportElement? Parent;
}