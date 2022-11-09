namespace VkQ.Domain.Reposts.ParticipantReport.ValueObjects;

public class ParticipantReportElement
{
    public ParticipantReportElement(string name, Guid participantId, bool isLeave)
    {
        Name = name;
        ParticipantId = participantId;
        IsLeave = isLeave;
    }

    public string Name { get; }
    public Guid ParticipantId { get; }
    public bool IsLeave { get; }
}