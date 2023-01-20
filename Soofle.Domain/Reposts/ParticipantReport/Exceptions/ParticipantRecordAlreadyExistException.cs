namespace Soofle.Domain.Reposts.ParticipantReport.Exceptions;

public class ParticipantRecordAlreadyExistException : Exception
{
    public ParticipantRecordAlreadyExistException(long vkId) : base(
        "Record for this participant already exist")
    {
        VkId = vkId;
    }

    public long VkId { get; }
}