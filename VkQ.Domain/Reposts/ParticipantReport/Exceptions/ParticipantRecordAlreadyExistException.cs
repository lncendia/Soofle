namespace VkQ.Domain.Reposts.ParticipantReport.Exceptions;

public class ParticipantRecordAlreadyExistException : Exception
{
    public ParticipantRecordAlreadyExistException() : base("Record for this participant already exist")
    {
    }
}