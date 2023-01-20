using Soofle.Domain.Participants.Entities;

namespace Soofle.Domain.Reposts.PublicationReport.DTOs;

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