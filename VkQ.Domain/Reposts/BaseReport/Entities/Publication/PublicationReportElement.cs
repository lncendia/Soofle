namespace VkQ.Domain.Reposts.BaseReport.Entities.Publication;

public abstract class PublicationReportElement
{
    protected PublicationReportElement(int id, string name, long vkId, Guid participantId, List<PublicationReportElement>? children)
    {
        Name = name;
        ParticipantId = participantId;
        Children = children;
        Id = id;
        VkId = vkId;
    }

    public int Id { get; }
    public string Name { get; }
    public long VkId { get; }
    public Guid ParticipantId { get; }
    public List<PublicationReportElement>? Children { get; }
    public bool IsAccepted { get; private set; }

    public void Accept() => IsAccepted = true;
}