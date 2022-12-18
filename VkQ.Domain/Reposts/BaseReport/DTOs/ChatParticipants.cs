using VkQ.Domain.Participants.Entities;

namespace VkQ.Domain.Reposts.BaseReport.DTOs;

public class ChatParticipants
{
    public ChatParticipants(IList<Participant> participants, string likeChatName)
    {
        Participants = participants;
        LikeChatName = likeChatName;
    }

    public string LikeChatName { get; }
    public IList<Participant> Participants { get; }
}