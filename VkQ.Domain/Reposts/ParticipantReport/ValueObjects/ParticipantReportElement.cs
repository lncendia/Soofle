namespace VkQ.Domain.Reposts.ParticipantReport.ValueObjects;

public class ParticipantReportElement
{
    public ParticipantReportElement(string name, long vkId, Guid participantId, bool isLeave)
    {
        Name = name;
        ParticipantId = participantId;
        IsLeave = isLeave;
        VkId = vkId;
    }

    public string Name { get; }
    public Guid ParticipantId { get; }
    public long VkId { get; }
    public bool IsLeave { get; }
}