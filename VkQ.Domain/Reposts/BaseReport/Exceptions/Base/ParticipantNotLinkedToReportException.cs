namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Base;

public class ParticipantNotLinkedToReportException : Exception
{
    public ParticipantNotLinkedToReportException(Guid id) : base($"Participant is not linked to report")
    {
        ParticipantId = id;
    }

    public Guid ParticipantId { get; }
}