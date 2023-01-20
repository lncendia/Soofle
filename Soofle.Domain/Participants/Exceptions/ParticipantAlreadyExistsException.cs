namespace Soofle.Domain.Participants.Exceptions;

public class ParticipantAlreadyExistsException : Exception
{
    public long VkId { get; }

    public ParticipantAlreadyExistsException(long vkId) : base("There is already such a participant")
    {
        VkId = vkId;
    }
}