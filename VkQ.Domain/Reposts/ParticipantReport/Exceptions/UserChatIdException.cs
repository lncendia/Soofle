namespace VkQ.Domain.Reposts.ParticipantReport.Exceptions;

public class UserChatIdException : Exception
{
    public UserChatIdException() : base("Chat id is not set")
    {
    }
}